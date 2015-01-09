using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.Common;
using TripViewBreda.GeoLocation;
using Windows.Devices.Geolocation;

namespace TripViewBredaUnitTest.Model.GeoLocation
{
    [TestClass]
    public class GPSTest
    {
        /// <summary>
        /// TODO: Go to the location Lovensdijkstraat 63. Put your pin location on the right side of the lovensdijkstraat.
        /// This method will test if the current GPS location is equal to the location of Avans Hogeschool Breda at lovensdijkstraat.
        /// </summary>
        [TestMethod]
        public async Task GPSObjectTest()
        {
            // arrange
            GPS gps = new GPS();
            Geopoint point = null;

            // act
            point = await gps.GetCurrentLocation(); // This method takes 5 sec

            // assert
            var actualLatitude = point.Position.Latitude;
            var actualLongitude = point.Position.Longitude;
            var minLatitude = 51.5850;
            var maxLatitude = 51.5862;
            var minLongitude = 4.7913;
            var maxLongitude = 4.7943;
            bool OnRightSideOfLovensdijkstraat = false;
            if (actualLatitude >= minLatitude && actualLatitude <= maxLatitude)
            {
                if (actualLongitude >= minLongitude && actualLongitude <= maxLongitude)
                {
                    OnRightSideOfLovensdijkstraat = true;
                }
            }
            Assert.IsNotNull(gps);
            Assert.IsNotNull(point);
            Assert.IsTrue(OnRightSideOfLovensdijkstraat);
        }
    }
}
