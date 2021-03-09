using Inspire.Properties;
using InspireData;

namespace Inspire.ViewModels
{
    public class ClockViewModel : BaseViewModel
    {
        private IClockService _clockService;
        private IClockData _clockData;
        
        /// <summary>
        /// Property bound to the Current Time TextBlock Text
        /// </summary>
        public string CurrentTime { get; set; }

        /// <summary>
        /// Property bound to the Time of Day Greeting TextBlock Text
        /// </summary>
        public string TimeOfDayGreeting { get; set; }

        /// <summary>
        /// Property bound to the 24 Hour Mode Checkbox
        /// </summary>
        private bool _is24HourMode = false;
        public bool Is24HourMode
        {
            get => _is24HourMode;
            set 
            { 
                _is24HourMode = value;
                Settings.Default.Is24HourMode = value;
                UpdateClockUi();
            }
        }

        public ClockViewModel(IClockService clockService)
        {
            _clockService = clockService;
            _is24HourMode = Settings.Default.Is24HourMode;
            _clockData = _clockService.GetClockData();
            UpdateClockUi();
            _clockService.StartClockUpdateTimer();
            _clockService.ClockUpdateEvent += ClockServiceClockUpdateEvent;
        }

        private void ClockServiceClockUpdateEvent(object sender, ClockEventArgs e)
        {
            _clockData = e.ClockData;
            UpdateClockUi();
        }

        private void UpdateClockUi()
        {
            string timeFormat = Is24HourMode ? "H:mm" : "h:mm tt";
            CurrentTime = _clockData.CurrentTime.ToString(timeFormat);
            TimeOfDayGreeting = GetTimeOfDayGreeting(_clockData.CurrentTime.Hour);
        }

        /// <summary>
        /// Method to provide a greeting depending on the time of day as follows:
        ///   Morning = 6am-12pm
        ///   Afternoon = 12-5pm
        ///   Evening = 5-10pm
        ///   Night = 10pm-6am
        /// </summary>
        /// <param name="hour">The current hour (military time)</param>
        /// <returns>The time of day greeting</returns>
        private string GetTimeOfDayGreeting(int hour)
        {
            string timeOfDay = "Night";
            if (hour >= 6 && hour < 12)
                timeOfDay = "Morning";
            else if (hour >= 12 && hour < 17)
                timeOfDay = "Afternoon";
            else if (hour >= 17 && hour < 22)
                timeOfDay = "Evening";
            return $"Good {timeOfDay}";
        }
    }
}
