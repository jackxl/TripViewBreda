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
        [TestMethod]
        public void SubjectObjectTest()
        {
            //arrange
            Subject subject = new Subject(new GPSPoint(51.592342, 4.548881), "test subject");

            //act
            OpeningHours openinghours = subject.GetOpeningHours();
            String info = subject.GetInformation();
            String name = subject.GetName();
            GPSPoint gpspoint = subject.GetLocation();

            ////Assert
            Assert.IsNotNull(openinghours);
            Assert.IsNotNull(info);
            Assert.IsNotNull(name);
            Assert.IsNotNull(gpspoint);
        }

    }
}
