using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.Model.Routes;

namespace TripViewBredaUnitTest.Model.Routes
{
    [TestClass]
    public class RouteTest
    {
        [TestMethod]
        public void RouteCollectionObjectTest()
        {
            // arrange
            List<IRoute> list = new List<IRoute>();
            int[] values = new int[] { 4, 25, 0, 1, 1 };

            // act
            list.Add(new Route.Cafes());
            list.Add(new Route.HistorischeKM());
            list.Add(new Route.Remaining());
            list.Add(new Route.School());
            list.Add(new Route.Tourist_Trail());


            // assert
            for (int i = 0; i < list.Count; i++)
            {
                Assert.IsFalse(IsNotNull(list[i]));
                Assert.AreEqual(values[i], Count(list[i]));
            }
        }
        private bool IsNotNull(IRoute route)
        { return route.GetSubjects() == null; }
        private int Count(IRoute route)
        { return route.GetSubjects().GetSubjects().Count; }
    }
}
