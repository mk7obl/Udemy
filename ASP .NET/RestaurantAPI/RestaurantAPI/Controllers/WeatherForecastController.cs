using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherForecastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var results = _service.Get(5, 5, 5);
            return results;
        }

        [HttpGet("currentDay/{max}")]
        public IEnumerable<WeatherForecast> Get2([FromQuery] int take, [FromRoute] int max)
        {
            var result = _service.Get(5, 5, 5);
            return result;
        }

        [HttpPost]
        public ActionResult<string> Hello([FromBody] string name)
        {
            //HttpContext.Response.StatusCode = 401;

            //return StatusCode(401, $"Hello {name}");

            return NotFound($"Hello {name}");
        }

    //    [Route("generate/{quantity}")]

        [HttpPost("generate/{quantity}")]
        public ActionResult<string> Generate([FromRoute] int quantity, [FromBody]WeatherForecast range)
        {

            int min = range.minT;
            int max = range.maxT;

            if (quantity > 0 && min<=max)
            {
                var result = _service;
                _service.Get(quantity, min, max);

                return Ok(200);
            }

            else
                return BadRequest(400);

        }


    }
}
