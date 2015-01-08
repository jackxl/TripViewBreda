using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.Controllers;
using TripViewBreda.GeoLocation;
using TripViewBreda.Model.Information;

namespace TripViewBredaUnitTest.Controllers
{
    [TestClass]
    public class DetailControllerTest
    {
        [TestMethod]
        public void DetailControllerObjectTest()
        {
            // arrange
            string name = "Test";
            GPSPoint gpspoint = new GPSPoint(51.01, 4.31);
            Subject subject = new Subject(gpspoint, name);
            var controller = new DetailController(subject);

            // assert
            Assert.IsNotNull(gpspoint);
            Assert.IsNotNull(subject);
            Assert.IsNotNull(controller);
            var actualName = subject.GetName();
            var expectedName = "Test";
            Assert.AreEqual(expectedName, actualName);

            // act
            controller.GetSubject().SetName("Testing");

            // assert
            expectedName = "Testing";
            actualName = controller.GetSubject().GetName();
            Assert.AreEqual(expectedName, actualName);
            actualName = subject.GetName();
            Assert.AreEqual(expectedName, actualName);

            var actualOpeningHours = controller.GetSubject().GetOpeningHours().ToString();
            var expectedOpeningHours = new OpeningHours().ToString();
            Assert.AreEqual(actualOpeningHours, expectedOpeningHours);

            // act
            var hours = new OpeningHours();
            hours.GetOpeningHours().Add(new OpenComponent(OpenComponent.Day.Maandag, new DateTime(10), new DateTime(20)));
            controller.GetSubject().SetOpeningsHours(hours);

            // assert
            var actualValue = controller.GetSubject().GetOpeningHours().GetOpeningHours().Count;
            var expectedValue = 1;
            var actualDay = controller.GetSubject().GetOpeningHours().GetOpeningHours()[0].GetDay();
            var expectedDay = OpenComponent.Day.Maandag;
            var actualOpenValue = controller.GetSubject().GetOpeningHours().GetOpeningHours()[0].GetOpenFrom().Ticks;
            var expectedOpenValue = 10;
            var actualTillValue = controller.GetSubject().GetOpeningHours().GetOpeningHours()[0].GetOpenTill().Ticks;
            var expectedTillValue = 20;

            Assert.AreEqual(expectedValue, actualValue);
            Assert.AreEqual(expectedDay, actualDay);
            Assert.AreEqual(expectedOpenValue, actualOpenValue);
            Assert.AreEqual(expectedTillValue, actualTillValue);
        }
    }
}
