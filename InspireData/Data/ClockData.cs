using System;

namespace InspireData
{
    /// <summary>
    /// Class containing all the Clock Data
    /// </summary>
    public class ClockData : IClockData
    {
        /// <summary>
        /// The Current Time and Date
        /// </summary>
        public DateTime CurrentTime { get; set; } = DateTime.Now;
    }
}
