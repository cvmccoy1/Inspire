using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspireData
{
    /// <summary>
    /// Interface for the providing Clock information and updates
    /// </summary>
    public interface IClockService
    {
        /// <summary>
        /// Gets the lastest Clock Data
        /// </summary>
        /// <returns>A <see cref="ClockData"/> object</returns>
        ClockData GetClockData();

        /// <summary>
        /// Starts the Clock Update Timer so that the <see cref="ClockUpdateEvent"/> will fire.
        /// </summary>
        /// <returns><b>true<b> if timer was started.<b>false</b> if already started.</returns>
        bool StartClockUpdateTimer();

        /// <summary>
        /// Event that fires every minute on the minute, providing an updated <see cref="ClockData"/> object.
        /// </summary>
        /// <remarks>The <see cref="StartClockUpdateTimer"/> must for be invoked.</remarks>
        event EventHandler<ClockEventArgs> ClockUpdateEvent;
    }

    public interface IClockData
    {
        /// <summary>
        /// The Current Time and Date
        /// </summary>
        DateTime CurrentTime { get; }
    }

    public class ClockEventArgs : EventArgs
    {
        public ClockData ClockData { get; set; } = new ClockData();
    }
}
