using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inspire.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Inspire.ViewModels.UnitTests
{
    [TestClass()]
    public class TemperatureTests
    {
        [TestMethod()]
        [DataRow(255.372, 0)]
        [DataRow(247.039, -15)]
        [DataRow(268.706, 24)]
        [DataRow(288.15, 59)]
        [DataRow(309.261, 97)]
        [DataRow(317.039, 111)]
        public void DisplayText_VerifyFahrenheitConvertion(double tempInKelving, int tempInFahrenheit)
        {
            // Assign
            int tempModeSelectIndex = (int)TemperatureMode.Fahrenheit;
            ObservableCollection<Temperature> collection = Temperature.PopulateCollection();
            string expectedDisplayName = $"{ tempInFahrenheit }° {collection[tempModeSelectIndex].Symbol}";

            // Act
            string actualDisplayName = collection[tempModeSelectIndex].DisplayText(tempInKelving);

            // Assert
            Assert.AreEqual(expectedDisplayName, actualDisplayName, "The Display Text is not correct");
        }

        [TestMethod()]
        [DataRow(255.372, -18)]
        [DataRow(247.039, -26)]
        [DataRow(268.706, -4)]
        [DataRow(288.15, 15)]
        [DataRow(309.261, 36)]
        [DataRow(317.039, 44)]
        public void DisplayText_VerifyCelsiusConvertion(double tempInKelving, int tempInFahrenheit)
        {
            // Assign
            int tempModeSelectIndex = (int)TemperatureMode.Celsius;
            ObservableCollection<Temperature> collection = Temperature.PopulateCollection();
            string expectedDisplayName = $"{ tempInFahrenheit }° {collection[tempModeSelectIndex].Symbol}";

            // Act
            string actualDisplayName = collection[tempModeSelectIndex].DisplayText(tempInKelving);

            // Assert
            Assert.AreEqual(expectedDisplayName, actualDisplayName, "The Display Text is not correct");
        }

        [TestMethod()]
        [DataRow(273.15, 273)]
        [DataRow(247.039, 247)]
        [DataRow(268.706, 269)]
        [DataRow(288.15, 288)]
        [DataRow(309.261, 309)]
        [DataRow(317.039, 317)]
        public void DisplayText_VerifyKelvinConvertion(double tempInKelving, int tempInFahrenheit)
        {
            // Assign
            int tempModeSelectIndex = (int)TemperatureMode.Kelvin;
            ObservableCollection<Temperature> collection = Temperature.PopulateCollection();
            string expectedDisplayName = $"{ tempInFahrenheit }° {collection[tempModeSelectIndex].Symbol}";

            // Act
            string actualDisplayName = collection[tempModeSelectIndex].DisplayText(tempInKelving);

            // Assert
            Assert.AreEqual(expectedDisplayName, actualDisplayName, "The Display Text is not correct");
        }
    }
}