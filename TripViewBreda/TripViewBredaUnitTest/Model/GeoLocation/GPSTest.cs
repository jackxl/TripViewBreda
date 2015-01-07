using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.GeoLocation;
using Windows.Devices.Geolocation;

namespace TripViewBredaUnitTest.Model.GeoLocation
{
    [TestClass]
    public class GPSTest
    {
        /// <summary>
        /// This method will test if the current GPS location is equal to the default simulator location.
        /// 
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
            //assertion of this test expects the simulator to have its default location wich is 47,-122
            var expectedLatitude = 47;
            var expectedLongitude = -122;
            var actualLatitude = (int)(point.Position.Latitude);
            var actualLongitude = (int)(point.Position.Longitude);
            Assert.IsNotNull(gps);
            Assert.IsNotNull(point);
            Assert.AreEqual(expectedLatitude, actualLatitude);
            Assert.AreEqual(expectedLongitude, actualLongitude);
        }
    }
}
