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

namespace RestaurantAPI.builder.Services
{

    public interface IDishService
    {
        IEnumerable<DishDto> GetAll(int restaurantId);
        int Create(int RestaurantId, CreateDishDto dto);
        DishDto GetById(int restaurantId, int dishId);
        void Delete(int restaurantId, int dishId);
        void RemoveAll(int restaurantId);

    }
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DishService> _logger;

        public DishService(RestaurantDbContext context, IMapper mapper, ILogger<DishService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishEntity = _mapper.Map<Dish>(dto);

            dishEntity.RestaurantId = restaurantId;

            _context.Dishes.Add(dishEntity);
            _context.SaveChanges();

            return dishEntity.Id;
        }

        public IEnumerable<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishDtos = _mapper.Map<List<DishDto>>(restaurant.Dishes);

            return dishDtos;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dish = _context
                .Dishes
                .FirstOrDefault(r => r.Id == dishId);

            if (dish is null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException("Dish not found");
            }

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }

        public void Delete(int restaurantId, int dishId)
        {
            var restaurant = _context
                .Restaurants
                .FirstOrDefault(r => r.Id == restaurantId);

            var dish = _context
                .Dishes
                .FirstOrDefault(r => r.Id == dishId);

            if (dish is null || dish.RestaurantId != restaurantId)
            {
                throw new NotImplementedException("Dish not found");
            }

            _context.Remove(dish);
            _context.SaveChanges();

            _logger.LogError($"Dish with ID {dishId} deleted from restaurant with ID {restaurantId}");
        }

        public void RemoveAll(int restaurantId)
        {

            var restaurant = GetRestaurantById(restaurantId);

            _context.RemoveRange(restaurant.Dishes);
            _context.SaveChanges();

            _logger.LogError($"Dishes removed from restaurant with ID {restaurantId}");

        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _context
                .Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            return restaurant;
        }
    }
}