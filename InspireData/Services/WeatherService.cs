using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InspireData
{
     /// <summary>
     /// Class to provide a service to retrieve Weather data.
     /// The service provides the following:
     ///   Current Temperature
     ///   Daily High Temperature
     ///   Daily Low Temperature
     ///   Brief Description of the weather condition
     ///   Icon Id representing the weather condition
     /// </summary>
    public class WeatherService : BaseHttpService<WeatherData>
    {
        /// <summary>
        /// Method to access the openweathermap website to request current weather data.
        /// </summary>
        /// <returns>A list of <see cref="WeatherData"/> objects.</returns>
        public static async Task<WeatherData> GetWeatherData(string city = "Boise")
        {
            return await GetDataFromService($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=a5700e16ef0c871e40c213ce39c40c58");
        }
    }
}
