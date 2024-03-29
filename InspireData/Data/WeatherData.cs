﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace InspireData
{
    /// <summary>
    /// Class containing all of the Weather data
    /// </summary>
    public class WeatherData : IWeatherData
    {
        /// <summary>
        /// The Current Temperature (in Kelvin)
        /// </summary>
        [JsonIgnore]
        public double CurrentTemperature
        {
            get => Main.Temp;
        }
        
        /// <summary>
        /// The Day's High Temperature (in Kelvin)
        /// </summary>
        [JsonIgnore]
        public double HighTemperature
        {
            get => Main.TempMax;
        }

        /// <summary>
        /// The Day's Low Temperature (in Kelvin)
        /// </summary>
        [JsonIgnore]
        public double LowTemperature
        {
            get => Main.TempMin;
        }

        /// <summary>
        /// URL to an icon representative of the current weather conditions
        /// </summary>
        [JsonIgnore]
        public string WeatherIconUrl
        {
            get => $"http://openweathermap.org/img/wn/{Weather[0].Icon}@2x.png";
        }

        /// <summary>
        /// A description of the current weather conditions (in plain text)
        /// </summary>
        [JsonIgnore]
        public string Description
        {
            get => Weather[0].Description;
        }

        // Generated by Xamasoft JSON Class Generator and then stripped out unneeded properties.
        // http://www.xamasoft.com/json-class-generator
        internal class InnerWeather
        {
            [JsonProperty("main")]
            public string Main { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("icon")]
            public string Icon { get; set; }
        }

        internal class InnerMain
        {

            [JsonProperty("temp")]
            public double Temp { get; set; }

            [JsonProperty("temp_min")]
            public double TempMin { get; set; }

            [JsonProperty("temp_max")]
            public double TempMax { get; set; }
        }


        [JsonProperty("weather")]
        internal IList<InnerWeather> Weather { get; set; }

        [JsonProperty("main")]
        internal InnerMain Main { get; set; }
    }
}


