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
        [TestMethod]
        public void GSPObjectTest()
        {
            GPS gps = new GPS();
            Geopoint point = null;
            //point = gps.GetCurrentLocation();

            Assert.IsNotNull(gps);
            Assert.IsNotNull(point);

            Assert.AreEqual((int)(point.Position.Latitude) - 1, 51);
        }
    }
}
