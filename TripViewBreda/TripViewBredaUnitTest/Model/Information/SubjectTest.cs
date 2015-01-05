using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.GeoLocation;
using TripViewBreda.Model.Information;

namespace TripViewBredaUnitTest.Model.Information
{
    [TestClass]
    public class SubjectTest
    {
        /// <summary>
        /// This Method will test is the subject if it can contain information, a name, openings time and GPS location.
        /// There will also be tested if the subject contains the right values and if they can be modified.
        /// </summary>
        [TestMethod]
        public void SubjectObjectTest()
        {
            // arrange
            Subject subject = new Subject(new GPSPoint(51.592342, 4.548881), "test subject");

            // act
            OpeningHours openinghours = subject.GetOpeningHours();
            String info = subject.GetInformation();
            String name = subject.GetName();
            GPSPoint gpspoint = subject.GetLocation();

            // assert
            var actualHour = openinghours;
            var actualInfo = info;
            var actualName = name;
            var actualPoint = gpspoint;

            Assert.IsNotNull(actualHour);
            Assert.IsNotNull(actualInfo);
            Assert.IsNotNull(actualName);
            Assert.IsNotNull(actualPoint);
        }

    }
}
