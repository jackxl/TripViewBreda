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
        [TestMethod]
        public void NavigationManagerObjectTest()
        {
            NavigationManager navManager = new NavigationManager();
            Route route = new RoutePlannerTest().RoutePlannerObjectTest();
            navManager.SetActiveRoute(route);

            Assert.IsNotNull(navManager);
            Assert.IsNotNull(route);
            Assert.IsNotNull(navManager.GetActiveRoute());

        }
    }
}
