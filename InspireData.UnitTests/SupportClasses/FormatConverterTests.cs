using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InspireData.Tests
{
    [TestClass()]
    public class FormatConverterTests
    {
        [TestMethod()]
        [DataRow("<p>This is a paragraph.<p>", "This is a paragraph.")]
        public void HTMLToText_VerifyCorrectConvertion(string html, string expectedPlainText)
        {
            // Assign

            // Act
            string actualPlainText = FormatConverter.HTMLToText(html);

            // Assert
            Assert.AreEqual(expectedPlainText, actualPlainText, "The HTML converter did not work correctly.");
        }
    }
}