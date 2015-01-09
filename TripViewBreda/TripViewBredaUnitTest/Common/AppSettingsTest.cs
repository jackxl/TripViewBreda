using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.Common;

namespace TripViewBredaUnitTest.Common
{
    [TestClass]
    public class ApplicationSettingsTest
    {
        [TestMethod]
        public void AppSettingsObjectTest()
        {
            // arrange
            var actual = AppSettings.APP_NAME;
            var expected = "TripView Breda";

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            actual = AppSettings.IsFirstLaunch;
            expected = "IsFirstLaunch";

            // assert
            Assert.AreEqual(expected, actual);

            // arrange
            actual = AppSettings.LastKnownLocation;
            expected = "LastKnownLocation";

            // assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void InstructionsObjectTest()
        {
            // arrange
            List<string[]> list = Information.FrequentlyAskedQuestions;

            // assert
            Assert.IsNotNull(list);

            var actualCount = list.Count;
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
