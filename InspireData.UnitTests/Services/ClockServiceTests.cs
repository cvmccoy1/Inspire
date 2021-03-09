using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace InspireData.Tests
{
    [TestClass()]
    public class ClockServiceTests
    {
        [TestMethod()]
        public void GetClockData_VerifyCorrectClockData()
        {
            const string TIME_FORMAT = "g";
            IClockService clockService = new ClockService();

            // Arrange
            string expectedTime = DateTime.Now.ToString(TIME_FORMAT);

            // Act
            string actualTime = clockService.GetClockData().CurrentTime.ToString(TIME_FORMAT);

            // Assert
            Assert.AreEqual(expectedTime, actualTime, $"Current Time provided doesn't appear to be correct.");
        }


        // Note that the following test could take up to a minute to run (or a bit more if it fails).
        [TestMethod()]
        public async Task StartClockUpdateTimer_VerifyClockUpdateEventFired()
        {
            IClockService clockService = new ClockService();
            SemaphoreSlim _signal = new SemaphoreSlim(0, 1);
            const string TIME_FORMAT = "HH:mm";
            string _newTime = string.Empty;

            // Arrange
            IClockData clockData = clockService.GetClockData();
            string expectedDateTime = clockData.CurrentTime.AddMinutes(1.0).ToString(TIME_FORMAT);
            EventHandler<ClockEventArgs> ClockUpdateEvent = (sender, e) =>
            {
                _newTime = e.ClockData.CurrentTime.ToString(TIME_FORMAT);
                _signal.Release();
            };

            // Act
            Assert.IsTrue(clockService.StartClockUpdateTimer(), "The Clock Timer should not have already been started.");
            clockService.ClockUpdateEvent += ClockUpdateEvent;
            try
            {
                // Assert
                Assert.IsTrue(await _signal.WaitAsync(65000), $"The Clock Service event did not fire within a minute.");
                Assert.AreEqual(expectedDateTime, _newTime, "The new time is not one minute past the original time.");
                Assert.IsFalse(clockService.StartClockUpdateTimer(), "The Clock Timer should have already been started.");
            }
            finally
            {
                clockService.ClockUpdateEvent -= ClockUpdateEvent;
            }
        }
    }
}