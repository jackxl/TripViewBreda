using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.Navigation;

namespace TripViewBredaUnitTest.Model.Navigation
{
    [TestClass]
    public class DefaultRoutesTest
    {
        [TestMethod]
        public void DefaultRoutesObjectTest()
        {
            DefaultRoutes defRoutes = new DefaultRoutes();
            List<Route> routes = new List<Route>();
            routes.Add(new RoutePlannerTest().RoutePlannerObjectTest());
            defRoutes.SetRoutes(routes);

            Assert.IsNotNull(defRoutes);
            Assert.IsNotNull(routes);
            Assert.IsNotNull(defRoutes.GetRoutes());
        }
    }
}
