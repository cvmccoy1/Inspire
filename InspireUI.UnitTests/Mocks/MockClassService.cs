using InspireData;
using System;
using System.Threading;

namespace InspireUI.UnitTests
{
    internal class MockClassService : IClockService
    {
        private readonly ClockData _clockData;
        private readonly int _interval;
        //
        public MockClassService(DateTime currentTime, int interval)
        {
            _clockData = new ClockData() { CurrentTime = currentTime };
            _interval = interval;
        }

        public ClockData GetClockData()
        {
            return _clockData;
        }

        private static readonly Timer _clockTimer = null;

        public bool StartClockUpdateTimer()
        {
            if (_clockTimer == null)
            {
                Timer _clockTimer = new Timer((ignore) =>
                {
                    ClockUpdateEvent?.Invoke(null, new MockClockEventArgs(_clockData.CurrentTime.AddMinutes(1)));
                }, null, _interval, _interval);
            }
            return false;
        }

        public event EventHandler<ClockEventArgs> ClockUpdateEvent;

        public class MockClockEventArgs : ClockEventArgs
        {
            public MockClockEventArgs(DateTime newTime)
            {
                ClockData.CurrentTime = newTime;
            }
        }

    }
}
