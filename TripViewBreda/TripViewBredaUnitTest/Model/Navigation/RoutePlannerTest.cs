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
    public class RoutePlannerTest
    {
        [TestMethod]
        public Route RoutePlannerObjectTest()
        {
            RoutePlanner planner = new RoutePlanner();

            GPSPoint startpoint = new GPSPoint(51.001, 4.025);
            GPSPoint endpoint = new GPSPoint(51.001, 4.030);

            Route route = planner.GenerateRoute(startpoint, endpoint);
            route.AddNode(new GPSPoint(51.001, 4.025));

            Assert.IsNotNull(planner);
            Assert.IsNotNull(startpoint);
            Assert.IsNotNull(endpoint);
            Assert.IsNotNull(route);

            Assert.AreEqual(((double)((int)Math.Round(route.Distance * 1000)) / 1000), 0.010);

            return route;
        }
    }
}
