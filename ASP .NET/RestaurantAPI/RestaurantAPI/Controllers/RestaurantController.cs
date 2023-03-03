using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
        {
            _restaurantService.Update(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
        
            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }


        [HttpGet]
        [Authorize(Policy = "AtLeast20")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _restaurantService.GetAll();
            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Restaurant> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);

            return Ok(restaurant);
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

