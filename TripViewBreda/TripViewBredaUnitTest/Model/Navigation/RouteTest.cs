using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.GeoLocation;
using TripViewBreda.Navigation;

namespace TripViewBredaUnitTest.Model.Navigation
{
    [TestClass]
    public class RouteTest
    {
        [TestMethod]
        public void RouteObjectTest()
        {
            double p1_x = 51.1;
            double p1_y = 4.3;
            double p2_x = 52.1;
            double p2_y = 5.3;
            Route route = new Route();

            GPSPoint gpspoint1 = new GPSPoint(p1_x, p1_y);
            route.AddNode(gpspoint1);

            GPSPoint gpspoint2 = new GPSPoint(p2_x, p2_y);
            route.AddNode(gpspoint2);

            Assert.IsNotNull(route);
            Assert.IsNotNull(route.GetRoutePoints());
            Assert.AreEqual(gpspoint2.GetLongitude() - gpspoint1.GetLongitude(), 1);
            Assert.AreEqual(gpspoint2.GetLattitude() - gpspoint1.GetLattitude(), 1);
        }
    }
}
