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
        /// <summary>
        /// This method tests if this object can contain a day, an openfrom and an opentill time.
        /// To test this there will be checked if the values are equal to the start value, so the value is saved in the component and not modified.
        /// </summary>
        [TestMethod]
        public void OpenComponentObjectTest()
        {
            //arrange
            OpenComponent.Day day = OpenComponent.Day.Maandag;
            DateTime openFrom = new DateTime();
            DateTime openTill = openFrom.AddHours(8);

            OpenComponent oc = new OpenComponent(day, openFrom, openTill);

            //act
            DateTime actualTill = oc.GetOpenTill();

            //Assert
            DateTime expectedTill = openFrom.AddHours(8);
            Assert.AreEqual(expectedTill, actualTill);

            //act
            OpenComponent.Day actualDay = oc.GetDay();

            //Assert
            OpenComponent.Day expectedDay = OpenComponent.Day.Maandag;
            Assert.AreEqual(expectedDay, actualDay);
        }
    }
}
