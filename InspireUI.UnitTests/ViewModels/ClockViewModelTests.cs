using Inspire.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace InspireUI.UnitTests
{
    [TestClass()]
    public class ClockViewModelTests
    {
        [TestMethod()]
        [DataRow(0, 0)]
        [DataRow(9, 59)]
        public void ClockViewModel_VerifyCorrectDisplayTime(int hour, int minute)
        {
            const string TIME_FORMAT_12 = "h:mm tt";
            const string TIME_FORMAT_24 = "H:mm";

            // Assign
            DateTime expectedCurrentTime = new DateTime(2000, 1, 1, hour, minute, 00);
            ClockViewModel viewModel = new ClockViewModel(new MockClassService(expectedCurrentTime, 100))
            {
                Is24HourMode = false
            };

            // Act & Assert
            Assert.AreEqual(expectedCurrentTime.ToString(TIME_FORMAT_12), viewModel.CurrentTime, "The Initial Display Time is not correct");
            Thread.Sleep(150);  // Wait a 'fake' minute to pass
            expectedCurrentTime = expectedCurrentTime.AddMinutes(1);
            Assert.AreEqual(expectedCurrentTime.ToString(TIME_FORMAT_12), viewModel.CurrentTime, "The Minute Later Display Time is not correct");
            viewModel.Is24HourMode = true;
            Assert.AreEqual(expectedCurrentTime.ToString(TIME_FORMAT_24), viewModel.CurrentTime, "The 24 Hour Mode Display Time is not correct");
        }

        [TestMethod()]
        [DataRow(5, 59, "Good Night", "Good Morning")]
        [DataRow(11, 59, "Good Morning", "Good Afternoon")]
        [DataRow(16, 59, "Good Afternoon", "Good Evening")]
        [DataRow(21, 59, "Good Evening", "Good Night")]
        public void ClockViewModel_VerifyCorrectTimeOfDayGreeting(int hour, int minute, string expectedFirstGreeting, string expectedSecondGreeting)
        {
            // Assign
            DateTime expectedCurrentTime = new DateTime(2000, 1, 1, hour, minute, 0);
            ClockViewModel viewModel = new ClockViewModel(new MockClassService(expectedCurrentTime, 100));

            // Act & Assert
            Assert.AreEqual(expectedFirstGreeting, viewModel.TimeOfDayGreeting, "The First Greeting is not correct");
            Thread.Sleep(150); // Wait a 'fake' minute to pass
            Assert.AreEqual(expectedSecondGreeting, viewModel.TimeOfDayGreeting, "The Second Greeting is not correct");
        }
    }
}