using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog.Web;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using RestaurantAPI.Authorization;

namespace RestaurantAPI.Services
{

    public interface IRestaurantService
    {
        RestaurantDto GetById(int id);
        IEnumerable <RestaurantDto> GetAll();
        int Create(CreateRestaurantDto dto, int userId);
        void Delete(int id, ClaimsPrincipal user);
        void Update(int id, UpdateRestaurantDto dto, ClaimsPrincipal user);
    }
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationService;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
        }

        public void Delete(int id, ClaimsPrincipal user)
        {
            _logger.LogError($"Restaurant with id: {id} DELETE action invoked");

            var restaurant = _dbContext
               .Restaurants
               .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(user, restaurant,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public RestaurantDto GetById (int id)
        {
            var restaurant = _dbContext
               .Restaurants
               .Include(r => r.Address)
               .Include(r => r.Dishes)
               .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) throw new NotFoundException("Restaurant not found"); ;

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
              .Restaurants
              .Include(r => r.Address)
              .Include(r => r.Dishes)
              .ToList();

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantsDtos;
        }

        public int Create(CreateRestaurantDto dto, int userId)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = userId;
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

        public void Update(int id, UpdateRestaurantDto dto, ClaimsPrincipal user)
        {

            var restaurant = _dbContext.Restaurants.Where(r => r.Id == id).FirstOrDefault<Restaurant>();

            if (restaurant != null)
            {
                restaurant.Name = dto.Name;
                restaurant.Description = dto.Description;
                restaurant.HasDelivery = dto.HasDelivery;
                _dbContext.SaveChanges();
            }

            else
                throw new NotFoundException("Restaurant not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(user, restaurant,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
        }
    }
}