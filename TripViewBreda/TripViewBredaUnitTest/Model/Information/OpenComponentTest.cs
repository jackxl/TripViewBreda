using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.Model.Information;

namespace TripViewBredaUnitTest.Model.Information
{
    [TestClass]
    public class OpenComponentTest
    {
        [TestMethod]
        public void OpenComponentObjectTest()
        {
            //arrange
            OpenComponent.Day day = OpenComponent.Day.Maandag;
            DateTime openFrom = new DateTime();
            DateTime openTill = openFrom.AddHours(8);

            OpenComponent oc = new OpenComponent(day, openFrom, openTill);

            //act
            DateTime actual = oc.getOpenTill();

            //Assert
            DateTime expected = openFrom.AddHours(8);
            Assert.AreEqual(expected, actual);

            //act
            OpenComponent.Day actualDay = oc.GetDay();

            //Assert
            OpenComponent.Day expectedDay = OpenComponent.Day.Maandag;
            Assert.AreEqual(expectedDay, actualDay);
        }
    }
}
