using Inspire.Properties;
using InspireData;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media.Imaging;
using static Inspire.ViewModels.Temperature;

namespace Inspire.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        public string CurrentTemperature { get; set; }

        public string HighTemperature { get; set; }

        public string LowTemperature { get; set; }

        public string Description { get; set; }

        public BitmapImage WeatherIcon { get; set; }

        public ObservableCollection<Temperature> Temperatures { get; set; }

        private Temperature _selectedTemperature;

        public Temperature SelectedTemperature
        {
            get => _selectedTemperature;
            set 
            {
                if (_selectedTemperature != value)
                {
                    _selectedTemperature = value;
                    UpdateWeatherUi();
                }
            }
        }

        [SuppressPropertyChangedWarnings]
        public int SelectedTemperatureIndex
        {
            set
            {
                if (value >= 0 && value != Settings.Default.TemperatureModeIndex)
                {
                    Settings.Default.TemperatureModeIndex = value;
                }
            }
        }

        public WeatherViewModel()
        {
            Temperatures = Temperature.PopulateCollection();
            SelectedTemperature = Temperatures[Settings.Default.TemperatureModeIndex];
            StartWeatherUpdateTimer();
        }

        private System.Timers.Timer _weatherTimer;

        /// <summary>
        /// Method to start a timer that checks the weather every 10 minutes.
        /// </summary>
        private void StartWeatherUpdateTimer()
        {
            if (_weatherTimer == null)
            {
                _weatherTimer = new System.Timers.Timer();
                _weatherTimer.AutoReset = true;
                _weatherTimer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsedEvent);
                _weatherTimer.Interval = 10 * 60 * 1000;  // Check Weather every 10 minutes for update
                _weatherTimer.Start();
            }
        }

        private void TimerElapsedEvent(object sender, ElapsedEventArgs e)
        {
            _syncContext.Post(o => UpdateWeatherUi(), null);
        }

        private async void UpdateWeatherUi()
        {
            try
            {
                WeatherData currentWeatherData = await WeatherService.GetWeatherData();
                Temperature selectedTemperatureMode = SelectedTemperature;
                ConverterDelegate converter = selectedTemperatureMode.Converter;  // Method used to convert the temperature from Kelvin to the selected temperature mode.
                string symbol = selectedTemperatureMode.Symbol;
                CurrentTemperature = $"{converter(currentWeatherData.CurrentTemperature)}° {symbol}";
                HighTemperature = $"{converter(currentWeatherData.HighTemperature)}° {symbol}";
                LowTemperature = $"{converter(currentWeatherData.LowTemperature)}° {symbol}";
                Description = currentWeatherData.Description;
                WeatherIcon = GetWeatherImage(currentWeatherData.WeatherIcon);
            }
            catch (Exception exp)
            {
                Debug.Fail($"Unable to update the Weather : {exp.Message}");
            }
        }

        /// <summary>
        /// Updates the Background Image from the Image Service
        /// </summary>
        private BitmapImage GetWeatherImage(string weatherId)
        {
            BitmapImage image = null;
            try
            {
                string url = $"http://openweathermap.org/img/wn/{weatherId}@2x.png";
                Uri uriSource = new Uri(url, UriKind.Absolute);
                image = new BitmapImage(uriSource);
            }
            catch (Exception exp)
            {
                Debug.Fail($"Unable to update the Weather : {exp.Message}");
            }
            return image;
        }
    }
}
