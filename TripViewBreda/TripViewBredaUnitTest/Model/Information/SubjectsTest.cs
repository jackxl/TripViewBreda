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
        /// <summary>
        /// This Method will test if you can add and remove Subject's and if the added Subject's are added and can be used for math
        /// </summary>
        [TestMethod]
        public void SubjectsObjectTest()
        {
            // arrange
            Subjects subjects = new Subjects();

            Subject subject1 = new Subject(new GPSPoint(51.000001, 51.000002), "Random");
            Subject subject2 = new Subject(new GPSPoint(51.000001, 51.000002), "Random");

            // act
            subjects.AddSubject(subject1);
            subjects.AddSubject(subject2);

            Subject testObject1 = subjects.GetSubjects()[0];
            Subject testObject2 = subjects.GetSubjects()[1];

            // Assert
            Assert.AreEqual(testObject1.GetName(), testObject2.GetName());
            Assert.AreEqual(testObject1.GetLocation().GetLattitude(), subjects.GetSubjects()[1].GetLocation().GetLattitude());
            Assert.AreEqual(testObject2.GetLocation().GetLongitude(), subjects.GetSubjects()[1].GetLocation().GetLongitude());

            // act
            subjects.RemoveSubject(testObject1);
            subjects.RemoveSubject(testObject2);

            // assert
            Assert.IsNotNull(subjects.GetSubjects());
            Assert.AreEqual(subjects.GetSubjects().Count, 0);
        }
    }
}
