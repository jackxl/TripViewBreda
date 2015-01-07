
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.GeoLocation;
using Windows.UI.Popups;

namespace TripViewBredaUnitTest.Model.GeoLocation
{
    [TestClass]
    public class GPSPointTest
    {
        /// <summary>
        /// This method will test is the GPSPoints can be created and are not null.
        /// Also will it test if the gpspoints can be substracted from each other.
        /// </summary>
        [TestMethod]
        public void GPSPointObjectTest()
        {
            // arrange
            GPSPoint point1 = new GPSPoint((511 / 10), (512 / 10));
            GPSPoint point2 = new GPSPoint((521 / 10), (522 / 10));

            // assert
            Assert.IsNotNull(point1);
            Assert.IsNotNull(point2);
            var expected = 0;
            Assert.AreNotEqual(expected, point1.GetLattitude());
            Assert.AreNotEqual(expected, point1.GetLongitude());
            Assert.AreNotEqual(expected, point2.GetLattitude());
            Assert.AreNotEqual(expected, point2.GetLongitude());

            // assert
            var actualValue1 = (point1.GetLongitude() - point1.GetLattitude());
            var actualValue2 = (point2.GetLongitude() - point2.GetLattitude());
            var expectedValue1 = (1 / 10);
            var expectedValue2 = (1 / 10);
            var actualDistance = point2.GetLongitude() - point1.GetLongitude();
            var expectedDistance = 1;
            Assert.AreEqual(expectedDistance, actualDistance);
            Assert.AreEqual(expectedValue1, actualValue1);
            Assert.AreEqual(expectedValue2, actualValue2);
        }
    }
}
