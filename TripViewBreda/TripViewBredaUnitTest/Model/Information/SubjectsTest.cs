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
    public class SubjectsTest
    {
        [TestMethod]
        public void SubjectsObjectTest()
        {
            Subjects subjects = new Subjects();

            Subject subject1 = new Subject(new GPSPoint(51.000001, 51.000002), "Random");
            Subject subject2 = new Subject(new GPSPoint(51.000001, 51.000002), "Random");
            subjects.AddSubject(subject1);
            subjects.AddSubject(subject2);


            // Assert
            Subject testObject1 = subjects.GetSubjects()[0];
            Subject testObject2 = subjects.GetSubjects()[1];
            Assert.AreEqual(testObject1.GetName(), testObject2.GetName());
            Assert.AreEqual(testObject1.GetLocation().GetLattitude(), subjects.GetSubjects()[1].GetLocation().GetLattitude());
            Assert.AreEqual(testObject2.GetLocation().GetLongitude(), subjects.GetSubjects()[1].GetLocation().GetLongitude());
            subjects.RemoveSubject(testObject1);
            subjects.RemoveSubject(testObject2);
            Assert.IsNotNull(subjects.GetSubjects());
            Assert.AreEqual(subjects.GetSubjects().Count, 0);
        }
    }
}
