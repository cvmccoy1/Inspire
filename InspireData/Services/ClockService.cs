using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace InspireData
{
    /// <summary>
    /// Class to provide a service to retrieve and update the clock data.
    /// At present, only provides the current time.
    /// </summary>
    public class ClockService
    {
        /// <summary>
        /// Retrieves the clock data
        /// </summary>
        /// <returns>A <see cref="ClockData"/> object</returns>
        public static ClockData GetClockData()
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
        public static bool StartClockUpdateTimer()
        {
            if (_clockTimer == null)
            {
                _clockTimer = new Timer();
                _clockTimer.AutoReset = false;
                _clockTimer.Elapsed += new ElapsedEventHandler(TimerElapsedEvent);
                _clockTimer.Interval = GetInterval();
                _clockTimer.Start();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Event that fires every minute on the minute, providing an updated <see cref="ClockData"/> object.
        /// </summary>
        public static event EventHandler<ClockEventArgs> ClockUpdateEvent = (sender, e) => 
        {
            // A body for debugging
        };

        private static Timer _clockTimer;

        /// <summary>
        /// Method called when the timer is fired.
        /// </summary>
        private static void TimerElapsedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            ClockUpdateEvent?.Invoke(null, new ClockEventArgs());
            _clockTimer.Interval = GetInterval();
            _clockTimer.Start();
        }

        /// <summary>
        /// Calculates when the next minute will occur (in milliseconds).
        /// </summary>
        private static double GetInterval()
        {
            DateTime now = DateTime.Now;
            return ((60 - now.Second) * 1000 - now.Millisecond);
        }
    }

    public class ClockEventArgs : EventArgs
    {
        public ClockData ClockData { get; set; } = new ClockData();
    }
}
