using Microsoft.VisualStudio.TestTools.UnitTesting;
using InspireData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            // Arrange
            DateTime expectedDateTime = DateTime.Now;

            // Act
            ClockData clockData = ClockService.GetClockData();

            //Assert
            Assert.AreEqual(expectedDateTime.ToString("g"), clockData.CurrentTime.ToString("g"), $"Current Time provided doesn't appear to be correct.");
        }

        // Note that the following test could take up to a minute to run (or a bit more if it fails).
        private readonly SemaphoreSlim _signal = new SemaphoreSlim(0, 1);
        private const string TIME_FORMAT = "HH:mm";
        private string _newTime;

        [TestMethod()]
        public async Task StartClockUpdateTimer_VerifyClockUpdateEventFired()
        {
            // Arrange
            ClockData clockData = ClockService.GetClockData();
            string expectedDateTime = clockData.CurrentTime.AddMinutes(1.0).ToString(TIME_FORMAT);
            EventHandler<ClockEventArgs> ClockUpdateEvent = (sender, e) =>
            {
                _newTime = e.ClockData.CurrentTime.ToString(TIME_FORMAT);
                _signal.Release();
            };

            // Act
            Assert.IsTrue(ClockService.StartClockUpdateTimer(), "The Clock Timer should not have already been started.");
            ClockService.ClockUpdateEvent += ClockUpdateEvent;
            try
            {
                // Assert
                Assert.IsTrue(await _signal.WaitAsync(65000), $"The Clock Service event did not fire within a minute.");
                Assert.AreEqual(expectedDateTime, _newTime, "The new time is not one minute past the original time.");
                Assert.IsFalse(ClockService.StartClockUpdateTimer(), "The Clock Timer should have already been started.");
            }
            finally
            {
                ClockService.ClockUpdateEvent -= ClockUpdateEvent;
            }
        }
    }
}