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
        /// <summary>
        /// This method tests if the constructor does not return null an d the routes can be added, removed and can be returned 
        /// </summary>
        [TestMethod]
        public void DefaultRoutesObjectTest()
        {
            // arrange
            DefaultRoutes defRoutes = new DefaultRoutes();
            List<Route> routes = new List<Route>();

            // act
            routes.Add(new RoutePlannerTest().RoutePlannerObjectTest());
            defRoutes.SetRoutes(routes);

            // assert
            var actualDefRoutes = defRoutes;
            var actualRoutes = routes;
            Assert.IsNotNull(actualDefRoutes);
            Assert.IsNotNull(actualRoutes);

            actualRoutes = defRoutes.GetRoutes();
            Assert.IsNotNull(actualRoutes);

            // act
            actualRoutes.RemoveAt(0);

            // assert
            var actualAmount = actualRoutes.Count;
            var expectedAmount = 0;
            Assert.AreEqual(expectedAmount, actualAmount);

            var actualDefAmount = defRoutes.GetRoutes().Count;
            Assert.AreEqual(expectedAmount, actualDefAmount);

            // act
            defRoutes = null;

            // assert
            actualDefRoutes = defRoutes;
            Assert.IsNull(defRoutes);
        }
    }
}
