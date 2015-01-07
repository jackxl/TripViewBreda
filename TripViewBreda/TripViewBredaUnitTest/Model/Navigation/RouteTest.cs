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
        /// <summary>
        /// This method will test if the Route can contain points and give the right points when asked for it.
        /// This method tests if the results are not null and if the substraction of point1 and point2 results 1.
        /// </summary>
        [TestMethod]
        public void RouteObjectTest()
        {
            // arrange
            double p1_x = 51.1;
            double p1_y = 4.3;
            double p2_x = 52.1;
            double p2_y = 5.3;
            Route route = new Route();

            // act
            GPSPoint gpspoint1 = new GPSPoint(p1_x, p1_y);
            route.AddNode(gpspoint1);

            GPSPoint gpspoint2 = new GPSPoint(p2_x, p2_y);
            route.AddNode(gpspoint2);

            // assert
            var actualRoute = route;
            var expected = 1;
            Assert.IsNotNull(actualRoute);
            Assert.IsNotNull(actualRoute.GetRoutePoints());

            var actualDistance = gpspoint2.GetLongitude() - gpspoint1.GetLongitude();
            Assert.AreEqual(expected, actualDistance);
            actualDistance = gpspoint2.GetLattitude() - gpspoint1.GetLattitude();
            Assert.AreEqual(expected, actualDistance);
        }
    }
}
