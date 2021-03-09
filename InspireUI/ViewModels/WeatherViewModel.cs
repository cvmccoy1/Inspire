using Inspire.Properties;
using InspireData;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media.Imaging;
using static Inspire.ViewModels.Temperature;

namespace Inspire.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        IWeatherService _weatherService;

        private const string LOCAL_CITY = "Eagle";

        /// <summary>
        /// Propety bound to the Current Temperature TextBlock
        /// </summary>
        public string CurrentTemperature { get; set; }

        /// <summary>
        /// Propety bound to the High Temperature TextBlock
        /// </summary>
        public string HighTemperature { get; set; }

        /// <summary>
        /// Propety bound to the High Temperature TextBlock
        /// </summary>
        public string LowTemperature { get; set; }

        /// <summary>
        /// Propety bound to the Current Temperature's Condition TextBlock
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Propety bound to the Current Temperature's Condition Image (Icon)
        /// </summary>
        public BitmapImage WeatherIcon { get; set; }

        /// <summary>
        /// Propety bound to the Temperature Format Combobox's List of Options
        /// </summary>
        public ObservableCollection<Temperature> Temperatures { get; set; }

        private Temperature _selectedTemperature;
        /// <summary>
        /// Property bound to the Temperature Format Combobox's Currently Selected Item
        /// </summary>
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

        /// <summary>
        /// Property bound to the Temperature Format Combobox's Currently Selected Index
        /// </summary>
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

        public WeatherViewModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;
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

        private void UpdateWeatherUi()
        {
            try
            {
                IWeatherData currentWeatherData = _weatherService.GetWeatherData(LOCAL_CITY);

                Temperature selectedTemperatureMode = SelectedTemperature;
                CurrentTemperature = selectedTemperatureMode.DisplayText(currentWeatherData.CurrentTemperature);
                HighTemperature = selectedTemperatureMode.DisplayText(currentWeatherData.HighTemperature); 
                LowTemperature = selectedTemperatureMode.DisplayText(currentWeatherData.LowTemperature);
                Description = currentWeatherData.Description;
                WeatherIcon = GetWeatherIconFromUrl(currentWeatherData.WeatherIconUrl);
            }
            catch (Exception exp)
            {
                Debug.Fail($"Unable to update the Weather : {exp.Message}");
            }
        }

        /// <summary>
        /// Updates the Background Image from the Image Service
        /// </summary>
        private BitmapImage GetWeatherIconFromUrl(string url)
        {
            BitmapImage image = null;
            try
            {
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
