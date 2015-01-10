using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Data.Json;
using TripViewBreda.Model.Information;
using TripViewBreda.GeoLocation;
using System.Diagnostics;
using TripViewBreda.Screens;

namespace TripViewBreda.Utilities
{
    class DataSource
    {
        private ObservableCollection<Subjects> _routes;
        const string filename = @"ms-appx:///routes.json";
        //const string filename = "routes.json";

        public DataSource()
        {
            _routes = new ObservableCollection<Model.Information.Subjects>();
        }

        public async Task<ObservableCollection<Model.Information.Subjects>> GetRoutes()
        {
            await ensureDataLoaded();
            return _routes;
        }

        private async Task ensureDataLoaded()
        {
            if (_routes.Count == 0)
                await getSubjectDataAsync();

            return;
        }

        private async Task getSubjectDataAsync()
        {
            if (_routes.Count != 0)
                return;

            try
            {
                Uri dataUri = new Uri(filename);
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
                string jsonText = await FileIO.ReadTextAsync(file);

                await SecondStep(jsonText);
            }
            catch (Exception)
            {
                _routes = new ObservableCollection<Model.Information.Subjects>();
                throw new NotImplementedException("Not Implemented Exception, Path: DataSource/getSubjectDataAsync()");
            }
        }
        private async Task SecondStep(string jsonText)
        {
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Subjects"].GetArray();
            foreach (JsonValue jsonSubjects in jsonArray)
            {
                string corruptKey = null;
                try
                {
                    JsonObject subjectsObject = jsonSubjects.GetObject();
                    Subjects subjects = new Subjects();
                    corruptKey = "Subjects Name";
                    subjects.SetName(subjectsObject["Name"].GetString());

                    //List<Model.Information.Subject> subjects = new List<Model.Information.Subject>();
                    corruptKey = "Subjects Items";
                    foreach (JsonValue jsonSubject in subjectsObject["Items"].GetArray())
                    {
                        JsonObject subjectObject = jsonSubject.GetObject();
                        corruptKey = "Subject Name";
                        string subjectName = subjectObject["Name"].GetString();
                        corruptKey = "Subject Information";
                        string subjectInformation = subjectObject["Information"].GetString();
                        corruptKey = "Subject ImageName";
                        string subjectImageName = subjectObject["ImageName"].GetString();
                        double lattitude = 0;
                        double longitude = 0;
                        try
                        {
                            lattitude = Double.Parse(subjectObject["Lattitude"].GetString());
                            longitude = Double.Parse(subjectObject["Longitude"].GetString());
                        }
                        catch
                        { Debug.WriteLine("Could not catch position from '" + subjectName + "'."); }
                        Subject subject = new Subject(new GPSPoint(lattitude, longitude), subjectName, subjectInformation, subjectImageName);
                        try
                        { subject.SetOpeningsHours(GetOpeningHoursFromJsonObject(subjectObject)); }
                        catch (Exception)
                        { Debug.WriteLine("Could not add Openinghours to : " + subjectName); }

                        subjects.AddSubject(subject);

                    }
                    _routes.Add(subjects);
                }
                catch (KeyNotFoundException) { Debug.WriteLine("Could not add subject. Key not found! (Key: " + corruptKey + ")"); }
            }
        }
        private OpeningHours GetOpeningHoursFromJsonObject(JsonObject subjectObject)
        {
            OpeningHours openinghours = new OpeningHours();
            foreach (JsonValue jsonOpeningHours in subjectObject["Open"].GetArray())
            {
                JsonObject jsonOpenHoursObject = jsonOpeningHours.GetObject();
                int openday = int.Parse(jsonOpenHoursObject["OpenDay"].GetString());
                long openfrom = long.Parse(jsonOpenHoursObject["OpenFrom"].GetString());
                long opentill = long.Parse(jsonOpenHoursObject["OpenTill"].GetString());
                OpenComponent opencomponent = new OpenComponent(OpenComponent.GetDay(openday), new DateTime(openfrom), new DateTime(opentill));
                //Debug.WriteLine("Loaded: " + opencomponent.ToString());
                openinghours.AddOpenComponent(opencomponent);
            }
            return openinghours;
        }
        public async void AddSubject(ObservableCollection<Model.Information.Subjects> subjects)
        {
            _routes = subjects;
            await saveSubjectDataAsync();
        }

        public async void DeleteSubject(ObservableCollection<Model.Information.Subjects> subjects)
        {
            _routes = subjects;
            await saveSubjectDataAsync();
        }

        private async Task saveSubjectDataAsync()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Model.Information.Subjects>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(filename,
                CreationCollisionOption.ReplaceExisting))
            {
                jsonSerializer.WriteObject(stream, _routes);
            }
        }
    }
}
