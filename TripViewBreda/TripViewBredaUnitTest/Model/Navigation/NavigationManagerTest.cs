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
        /// This method does test if the NavigationManager can set and get the current Active Route and this doesn't return null
        /// The currentstep can be increesed an decreesed.
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
            var actualValue = navManager.CurrentStep;
            var expectedValue = 1;
            Assert.AreEqual(expectedValue, actualValue);

            // act
            navManager.IncreesStep();
            navManager.IncreesStep();
            navManager.DecreesStep();

            // assert
            actualValue = navManager.CurrentStep;
            expectedValue = 2;
            Assert.AreEqual(expectedValue, actualValue);

        }
    }
}
