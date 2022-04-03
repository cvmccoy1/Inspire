using System;
using System.Timers;

namespace InspireData
{
    /// <summary>
    /// Class to provide a service to retrieve and update the clock data.
    /// At present, only provides the current time.
    /// </summary>
    public class ClockService : IClockService
    {
        /// <summary>
        /// Retrieves the clock data
        /// </summary>
        /// <returns>A <see cref="ClockData"/> object</returns>
        public ClockData GetClockData()
        {
            ClockData clockData = new ClockData()
            {
                CurrentTime = DateTime.Now
            };
            return clockData;
        }

        /// <summary>
        /// Starts up an one minute timer
        /// </summary>
        /// <returns><b>true<b> if timer was started.<b>false</b> if already started.</returns>
        public bool StartClockUpdateTimer()
        {
            if (_clockTimer == null)
            {
                _clockTimer = new Timer
                {
                    AutoReset = false
                };
                _clockTimer.Elapsed += TimerElapsedEvent;
                _clockTimer.Interval = GetInterval();
                _clockTimer.Start();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Event that fires every minute on the minute, providing an updated <see cref="ClockData"/> object.
        /// </summary>
        public event EventHandler<ClockEventArgs> ClockUpdateEvent = (sender, e) =>
        {
            // A body for debugging
        };

        private const int SECONDS_PER_MINUTE = 60;
        private static Timer _clockTimer;

        /// <summary>
        /// Method called when the timer is fired.
        /// </summary>
        private void TimerElapsedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            ClockUpdateEvent?.Invoke(null, new ClockEventArgs());
            _clockTimer.Interval = GetInterval();
            _clockTimer.Start();
        }

        /// <summary>
        /// Calculates when the next minute will occur (in milliseconds).
        /// </summary>
        private double GetInterval()
        {
            DateTime now = DateTime.Now;
            return ((SECONDS_PER_MINUTE - now.Second) * 1000) - now.Millisecond;
        }
    }
}
