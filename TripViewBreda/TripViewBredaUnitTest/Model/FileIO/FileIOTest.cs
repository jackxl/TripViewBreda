using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.GeoLocation;
using TripViewBreda.Model.FileIO;
using TripViewBreda.Model.Information;

namespace TripViewBredaUnitTest.Model.FileIO
{
    [TestClass]
    public class FileIOTest
    {
        [TestMethod]
        public void FileIO_JsonIOTest()
        {
            // arrange
            TripViewBreda.Model.FileIO.FileIO fileIO = new JsonIO();
            TripViewBreda.Model.FileIO.JsonIO jsonIO = new JsonIO();

            GPSPoint gpspoint = new GPSPoint(51.0001, 4.00001);
            Subject subject = new Subject(gpspoint, "home");
            Subjects subjects = new Subjects();

            // act
            subjects.AddSubject(subject);
            bool fileIOSucces = fileIO.write(subjects);
            bool jsonIOSucces = jsonIO.write(subjects);


            // assert
            bool actualBool = fileIOSucces;
            bool expectedBool = true;
            Assert.AreEqual(expectedBool, actualBool);
            actualBool = jsonIOSucces;
            Assert.AreEqual(expectedBool, actualBool);

            // act
            fileIO.read();
            fileIOSucces = fileIO.write(subjects);
            jsonIO.read();
            jsonIOSucces = fileIO.write(subjects);

            // assert
            actualBool = fileIOSucces;
            Assert.AreEqual(expectedBool, actualBool);
            actualBool = jsonIOSucces;
            Assert.AreEqual(expectedBool, actualBool);

            // act
            ObservableCollection<Subjects> actualResult = jsonIO.GetSubjects();
            ObservableCollection<Subjects> expectedResult = new System.Collections.ObjectModel.ObservableCollection<Subjects>();
            expectedResult.Add(subjects);

            // assert
            Assert.AreEqual(expectedResult[0], actualResult[0]);
            var expectedName = "home";
            var actualName = actualResult[0].GetSubjects()[0].GetName();
            Assert.AreEqual(expectedName, actualName);

            // act
            jsonIO.delete(subjects);

            // assert
            var actualCount = jsonIO.GetSubjects().Count;
            var expectedCount = 0;
            Assert.AreEqual(expectedCount, actualCount);

        }
    }
}
