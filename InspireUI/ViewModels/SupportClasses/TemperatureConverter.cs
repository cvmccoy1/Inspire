using System;
using System.Collections.ObjectModel;

namespace Inspire.ViewModels
{
    public enum TemperatureMode
    {
        Fahrenheit,
        Celsius,
        Kelvin,
    }
    /// <summary>
    /// Helper class used by the Temperature Mode selection combobox.
    /// Provides methods for converting from one mode to another.
    /// Currently, converts only from kelvin.
    /// </summary>
    public class Temperature
    {
        public delegate int ConverterDelegate(double tempInKelvin);

        public string Name { get; set; }

        public string Symbol { get; set; }

        public ConverterDelegate Converter { get; set; }

        public string DisplayText(double tempInKelving)
        {
            return $"{Converter(tempInKelving)}° {Symbol}";
        }

        public static ObservableCollection<Temperature> PopulateCollection()
        {
            return new ObservableCollection<Temperature>()
            {
                new Temperature() { Name = "Fahrenheit (F)", Symbol = "F", Converter = (t) => (int)Math.Round((t * 9 / 5) - 459.67)},
                new Temperature() { Name = "Celsius (C)", Symbol = "C", Converter = (t) => (int)Math.Round(t -273.15)},
                new Temperature() { Name = "Kelvin (K)", Symbol = "K", Converter = (t) => (int)Math.Round(t)},
            };
        }
    }
}
