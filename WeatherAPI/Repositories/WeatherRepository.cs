﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using WeatherAPI.Class;

namespace WeatherAPI.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        WeatherResponse IWeatherRepository.GetForecast(string city)
        {
            string IDOWeather = Config.Constants.OPEN_WEATHER_APPID;
            // Connection String
            var client = new RestClient($"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&APPID={IDOWeather}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                // Deserialize the string content into JToken object
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                // Deserialize the JToken object into our WeatherResponse Class
                return content.ToObject<WeatherResponse>();
            }

            return null;
        }
    }
}
