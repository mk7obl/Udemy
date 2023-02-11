using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI
{
    public interface IWeatherForecastService
    {

        IEnumerable<WeatherForecast> Get(int results, int minTemperature, int maxTemperature);
    }
}
