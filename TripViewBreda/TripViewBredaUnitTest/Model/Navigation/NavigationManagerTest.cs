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
    public class NavigationManagerTest
    {
        /// <summary>
        /// This method does ????
        /// </summary>
        [TestMethod]
        public void NavigationManagerObjectTest()
        {
            // arrange
            NavigationManager navManager = new NavigationManager();
            Route route = new RoutePlannerTest().RoutePlannerObjectTest();

            // act
            navManager.SetActiveRoute(route);

            // assert
            var actualManager = navManager;
            var actualRoute = route;
            var actualActiveRoute = navManager.GetActiveRoute();
            Assert.IsNotNull(actualManager);
            Assert.IsNotNull(actualRoute);
            Assert.IsNotNull(actualActiveRoute);

        }
    }
}
