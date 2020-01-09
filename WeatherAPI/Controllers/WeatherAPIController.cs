using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.Class;
using WeatherAPI.Models;
using WeatherAPI.Repositories;

namespace WeatherAPI.Controllers
{
    public class WeatherAPIController : Controller
    {
        private readonly IWeatherRepository _weatherRepository;
        public WeatherAPIController(IWeatherRepository weatherRepo)
        {
            _weatherRepository = weatherRepo;
        }
        public IActionResult SearchCity()
        {
            var viewModel = new SearchCity();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SearchCity(SearchCity model)
        {
            if (ModelState.IsValid)
                return RedirectToAction("City", "WeatherAPI", new { city = model.CityName });
            return View(model);
        }

        public IActionResult City(string city)
        {
            WeatherResponse weatherResponse = _weatherRepository.GetForecast(city);
            City viewModel = new City();

            if (weatherResponse != null)
            {
                viewModel.Name = weatherResponse.Name;
                viewModel.Humidity = weatherResponse.Main.Humidity;
                viewModel.Pressure = weatherResponse.Main.Pressure;
                viewModel.Temp = weatherResponse.Main.Temp;
                viewModel.Weather = weatherResponse.Weather[0].Main;
                viewModel.Wind = weatherResponse.Wind.Speed;
            }
            return View(viewModel);
        }
    }
}
