using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherAPI.Class;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers
{
    public class OpenWeatherMapMvcController : Controller
    {
        public OpenWeatherMap FillCity()
        {
            OpenWeatherMap openWeatherMap = new OpenWeatherMap();
            openWeatherMap.cities = new Dictionary<string, string>();
            openWeatherMap.cities.Add("Melbourne", "7839805");
            openWeatherMap.cities.Add("Auckland", "2193734");
            openWeatherMap.cities.Add("New Delhi", "1261481");
            openWeatherMap.cities.Add("Abu Dhabi", "292968");
            openWeatherMap.cities.Add("Lahore", "1172451");
            return openWeatherMap;
        }

        public IActionResult Index()
        {
            OpenWeatherMap openWeatherMap = FillCity();
            return View(openWeatherMap);
        }

        [HttpPost("weather")]
        public IActionResult Index(OpenWeatherMap openWeatherMap, string cities)
        {
            openWeatherMap = FillCity();
            if (cities != null)
            {
                string apiKey = "a8e7f987bc51700c7949df80ee6af321";
                HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?id=" 
                    + cities + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;

                string apiResponse = "";
                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                }
                ResponseWeather rootObject = JsonSerializer.Deserialize<ResponseWeather>(apiResponse);

                StringBuilder sb = new StringBuilder();
                sb.Append("<table><tr><th>Weather Description</th></tr>");
                sb.Append("<tr><td>City:</td><td>" +
                rootObject.name + "</td></tr>");
                sb.Append("<tr><td>Country:</td><td>" +
                rootObject.sys.country + "</td></tr>");
                sb.Append("<tr><td>Wind:</td><td>" +
                rootObject.wind.speed + " Km/h</td></tr>");
                sb.Append("<tr><td>Current Temperature:</td><td>" +
                rootObject.main.temp + " °C</td></tr>");
                sb.Append("<tr><td>Humidity:</td><td>" +
                rootObject.main.humidity + "</td></tr>");
                sb.Append("<tr><td>Weather:</td><td>" +
                rootObject.weather[0].description + "</td></tr>");
                sb.Append("</table>");
                openWeatherMap.apiResponse = sb.ToString();
            }
            return View(openWeatherMap);
        }
    }
}
