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
    public class OpeningHoursTest
    {
        [TestMethod]
        public void OpeningHoursObjectTest()
        {
            OpeningHours hours = new OpeningHours();

            OpenComponent.Day day1 = OpenComponent.Day.Maandag;
            DateTime open1 = new DateTime(8);
            DateTime closed1 = new DateTime(22);
            OpenComponent.Day day2 = OpenComponent.Day.Maandag;
            DateTime open2 = new DateTime(8);
            DateTime closed2 = new DateTime(21);

            OpenComponent openComponent1 = new OpenComponent(day1, open1, closed1);
            OpenComponent openComponent2 = new OpenComponent(day2, open2, closed2);

            hours.AddOpenComponent(openComponent1);
            hours.AddOpenComponent(openComponent2);

            //Assert
            CheckNull(hours);
            CheckEquals(hours);
        }
        private void CheckNull(OpeningHours hours)
        {
            Assert.IsNotNull(hours);
            Assert.IsNotNull(hours.GetOpeningHours());
            Assert.IsNotNull(hours.GetOpeningHours()[0]);
            Assert.IsNotNull(hours.GetOpeningHours()[1]);
        }
        private void CheckEquals(OpeningHours hours)
        {
            Assert.AreEqual(hours.GetOpeningHours()[0].GetDay(), hours.GetOpeningHours()[1].GetDay());
            Assert.AreEqual(hours.GetOpeningHours()[0].GetOpenFrom(), hours.GetOpeningHours()[1].GetOpenFrom());
            Assert.AreNotEqual(hours.GetOpeningHours()[0].getOpenTill(), hours.GetOpeningHours()[1].getOpenTill());
        }
    }
}
