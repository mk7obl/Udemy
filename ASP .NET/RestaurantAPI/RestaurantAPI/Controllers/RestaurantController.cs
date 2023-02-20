using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;

        public RestaurantController(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .ToList();

            return Ok(restaurants);
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

            return Ok(restaurant);
        }

        [HttpGet("random/{results}")]
        public ActionResult<Restaurant> Get2([FromRoute]int results)
        {
           // var count = restaurant.Restaurants.Count();
            Random rand = new Random();

            List<Restaurant> list = new List<Restaurant>();

            for(int i=1; i<=results; i++)
            {
                int j = rand.Next(1, 3);
                var restaurant = _dbContext
                    .Restaurants
                    .FirstOrDefault(r => r.Id == j);

                list.Add(restaurant);
            }

            return Ok(list);

        }
    }
}

