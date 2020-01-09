using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.Class;

namespace WeatherAPI.Repositories
{
    public interface IWeatherRepository
    {
        WeatherResponse GetForecast(string city);
    }
}
