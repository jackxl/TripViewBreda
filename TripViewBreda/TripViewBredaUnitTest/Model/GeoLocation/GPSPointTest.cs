
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
        [TestMethod]
        public void GPSPointObjectTest()
        {
            GPSPoint point1 = new GPSPoint((511 / 10), (512 / 10));
            GPSPoint point2 = new GPSPoint((521 / 10), (522 / 10));

            CheckNotNull(point1, point2);
            CheckMath(point1, point2);
        }
        private void CheckNotNull(GPSPoint point1, GPSPoint point2)
        {
            Assert.IsNotNull(point1);
            Assert.IsNotNull(point2);
            Assert.AreNotEqual(point1.GetLattitude(), 0);
            Assert.AreNotEqual(point1.GetLongitude(), 0);
            Assert.AreNotEqual(point2.GetLattitude(), 0);
            Assert.AreNotEqual(point2.GetLongitude(), 0);
        }
        private void CheckMath(GPSPoint point1, GPSPoint point2)
        {
            double value1 = (point1.GetLongitude() - point1.GetLattitude());
            double value2 = (point2.GetLongitude() - point2.GetLattitude());
            Assert.AreEqual(point2.GetLongitude() - point1.GetLongitude(), 1);
            Assert.AreEqual(value1, (1 / 10));
            Assert.AreEqual(value2, (1 / 10));
        }
    }
}
