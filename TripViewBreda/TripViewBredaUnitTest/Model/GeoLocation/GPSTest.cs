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
        public async Task GSPObjectTest()
        {
            GPS gps = new GPS();
            Geopoint point = null;
            point = await gps.GetCurrentLocation();

            Assert.IsNotNull(gps);
            Assert.IsNotNull(point);

            //assertion of this test expects the simulator to have its default location wich is 47,-122
            Assert.AreEqual(47, (int)(point.Position.Latitude)); // 47
            Assert.AreEqual(-122, (int)(point.Position.Longitude)); // 4.47
        }
    }
}
