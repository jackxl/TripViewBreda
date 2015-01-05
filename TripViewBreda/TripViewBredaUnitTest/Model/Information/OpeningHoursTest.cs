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
        /// <summary>
        /// This method does ????
        /// </summary>
        [TestMethod]
        public void OpeningHoursObjectTest()
        {
            // arrange
            OpeningHours hours = new OpeningHours();

            OpenComponent.Day day1 = OpenComponent.Day.Maandag;
            OpenComponent.Day day2 = OpenComponent.Day.Maandag;

            DateTime open1 = new DateTime(8);
            DateTime closed1 = new DateTime(22);
            DateTime open2 = new DateTime(8);
            DateTime closed2 = new DateTime(21);

            // act
            OpenComponent openComponent1 = new OpenComponent(day1, open1, closed1);
            OpenComponent openComponent2 = new OpenComponent(day2, open2, closed2);

            hours.AddOpenComponent(openComponent1);
            hours.AddOpenComponent(openComponent2);

            // assert
            var actualHours = hours;
            Assert.IsNotNull(actualHours);
            Assert.IsNotNull(actualHours.GetOpeningHours());
            Assert.IsNotNull(actualHours.GetOpeningHours()[0]);
            Assert.IsNotNull(actualHours.GetOpeningHours()[1]);

            OpenComponent.Day actualDay = hours.GetOpeningHours()[1].GetDay();
            OpenComponent.Day expectedDay = hours.GetOpeningHours()[0].GetDay();
            Assert.AreEqual(expectedDay, actualDay);
            DateTime actualFrom = hours.GetOpeningHours()[0].GetOpenFrom();
            DateTime expectedFrom = hours.GetOpeningHours()[1].GetOpenFrom();
            Assert.AreEqual(expectedFrom, actualFrom);
            DateTime actualTill = hours.GetOpeningHours()[0].GetOpenTill();
            DateTime expectedTill = hours.GetOpeningHours()[1].GetOpenTill();
            Assert.AreNotEqual(expectedTill, actualTill);

            // act
            actualTill = new DateTime(actualTill.Ticks - 1);

            // assert
            Assert.AreEqual(expectedTill, actualTill);
        }
    }
}
