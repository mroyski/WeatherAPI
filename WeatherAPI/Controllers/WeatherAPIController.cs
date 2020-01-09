using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers
{
    public class WeatherAPIController : Controller
    {
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
            City viewModel = new City();
            return View(viewModel);
        }
    }
}
