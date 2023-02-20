using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantController(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r=> r.Address) 
                .Include(r=> r.Dishes)
                .ToList();

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get([FromRoute] int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            var restaurantDto = _mapper.Map<List<RestaurantDto>>(restaurant);

            return Ok(restaurantDto);
        }

        //[HttpGet("random/{results}")]
        //public ActionResult<Restaurant> Get2([FromRoute]int results)
        //{
        //   // var count = restaurant.Restaurants.Count();
        //    Random rand = new Random();

        //    List<Restaurant> list = new List<Restaurant>();

        //    for(int i=1; i<=results; i++)
        //    {
        //        int j = rand.Next(1, 3);
        //        var restaurant = _dbContext
        //            .Restaurants
        //            .FirstOrDefault(r => r.Id == j);

        //        list.Add(restaurant);
        //    }

        //    return Ok(list);

        //}
    }
}

