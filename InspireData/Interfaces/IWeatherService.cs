using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireData
{
    /// <summary>
    /// Interface for 
    /// </summary>
    public interface IWeatherService
    {
        IWeatherData GetWeatherData(string city);
    }

    public interface IWeatherData
    {
        /// <summary>
        /// The Current Temperature (in Kelvin)
        /// </summary>
        double CurrentTemperature { get; }

        /// <summary>
        /// The Day's High Temperature (in Kelvin)
        /// </summary>
        double HighTemperature { get; }

        /// <summary>
        /// The Day's Low Temperature (in Kelvin)
        /// </summary>
        double LowTemperature { get; }

        /// <summary>
        /// Url to an icon representative of the current weather conditions
        /// </summary>
        string WeatherIconUrl { get; }

        /// <summary>
        /// A descripton of the current weather conditions (in plain text)
        /// </summary>
        string Description { get; }
    }
}
