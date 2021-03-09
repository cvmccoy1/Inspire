using System.Collections.ObjectModel;

namespace Inspire.ViewModels
{
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

        private static int KelvinToFahrenheitConverter(double tempInKelving) => (int)(tempInKelving * 9 / 5 - 459.67 + 0.5);

        private static int KelvinToCelsiusConverter(double tempInKelving) => (int)(tempInKelving -273.15 + 0.5);

        private static int KelvinToKelvinConverter(double tempInKelving) => (int)(tempInKelving + 0.5);

        public static ObservableCollection<Temperature> PopulateCollection()
        {
            return new ObservableCollection<Temperature>()
            {
                new Temperature() { Name = "Fahrenheit (F)", Symbol = "F", Converter = KelvinToFahrenheitConverter},
                new Temperature() { Name = "Celsius (C)", Symbol = "C", Converter = KelvinToCelsiusConverter},
                new Temperature() { Name = "Kelvin (K)", Symbol = "K", Converter = KelvinToKelvinConverter},
            };
        }
    }
}
