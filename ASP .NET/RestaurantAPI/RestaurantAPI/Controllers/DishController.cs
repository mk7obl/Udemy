using System.Text;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{

    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        [HttpPost]
        public ActionResult Post([FromRoute]int restaurantId,[FromBody] CreateDishDto dto)
        {
            var newDishId = _dishService.Create(restaurantId, dto);

            return Created($"api/{restaurantId}/dish/{newDishId}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<DishDto>> GetAll()
        {
            var dishDtos = _dishService.GetAll();
            
            return Ok(dishDtos);
        }

        [Route("{dishId}")]
        [HttpGet]
        public ActionResult GetDish([FromRoute]int id)
        {
            var dishDto = _dishService.GetDish(id);

            return Ok(dishDto);
        }

    }
}