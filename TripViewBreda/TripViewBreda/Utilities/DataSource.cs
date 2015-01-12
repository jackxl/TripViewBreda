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
        private ObservableCollection<Subjects> _events;
        const string routefilename = @"ms-appx:///jsonFiles/routes.json";
        const string eventfilename = @"ms-appx:///jsonFiles/events.json";
        //const string filename = "routes.json";

        public DataSource()
        {
            _routes = new ObservableCollection<Model.Information.Subjects>();
            _events = new ObservableCollection<Subjects>();
        }

        public async Task<ObservableCollection<Model.Information.Subjects>> GetRoutes()
        {
            await ensureSubjectDataLoaded();
            return _routes;
        }
        public async Task<ObservableCollection<Subjects>> GetEvents()
        {
            await ensureEventDataLoaded();
            return _events;
        }

        private async Task ensureSubjectDataLoaded()
        {
            if (_routes.Count == 0)
                await getSubjectDataAsync();
            return;
        }
        private async Task ensureEventDataLoaded()
        {
            if (_events.Count == 0)
                await getEventDataAsync();
            return;
        }

        private async Task getSubjectDataAsync()
        {
            if (_routes.Count != 0)
                return;

            try
            {
                Uri dataUri = new Uri(routefilename);
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
                string jsonText = await FileIO.ReadTextAsync(file);

                await LoadSubjectsFromFileToCollection(jsonText, _routes);
            }
            catch (Exception)
            {
                _routes = new ObservableCollection<Model.Information.Subjects>();
                throw new NotImplementedException("Not Implemented Exception, Path: DataSource/getSubjectDataAsync()");
            }
        }
        private async Task getEventDataAsync()
        {
            if (_events.Count != 0)
                return;
            try
            {
                Uri dataUri = new Uri(eventfilename);
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
                string jsonText = await FileIO.ReadTextAsync(file);

                await LoadSubjectsFromFileToCollection(jsonText, _events);
            }
            catch (Exception)
            {
                _events = new ObservableCollection<Subjects>();
                throw new NotImplementedException("Not Implemented Exception, Path: DataSource/getEventDataAsync()");
            }
        }
        private async Task LoadSubjectsFromFileToCollection(string jsonText, ObservableCollection<Subjects> toCollection)
        {
            string subjectName = "'Unknown'";
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
                        subjectName = subjectObject["Name"].GetString();
                        corruptKey = "Subject Information";
                        string subjectInformation = subjectObject["Information"].GetString();
                        corruptKey = "Subject ImageName";
                        string subjectImageName = subjectObject["ImageName"].GetString();
                        corruptKey = "Subject YoutubeVideoID";
                        string subjectVideoID = subjectObject["YoutubeVideoID"].GetString();
                        double lattitude = 0;
                        double longitude = 0;
                        try
                        {
                            lattitude = Double.Parse(subjectObject["Lattitude"].GetString());
                            longitude = Double.Parse(subjectObject["Longitude"].GetString());
                        }
                        catch
                        { Debug.WriteLine("Could not catch position from '" + subjectName + "'."); }
                        Subject subject = new Subject(new GPSPoint(lattitude, longitude), subjectName, subjectInformation, subjectImageName, subjectVideoID);
                        try
                        { subject.SetOpeningsHours(GetOpeningHoursFromJsonObject(subjectObject)); }
                        catch (Exception)
                        { Debug.WriteLine("Could not add Openinghours to : " + subjectName); }
                        subjects.AddSubject(subject);

                    }
                    toCollection.Add(subjects);
                }
                catch (KeyNotFoundException) { Debug.WriteLine("Could not add subject. Key not found! (Key: " + subjectName + " - " + corruptKey + ")"); }
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

        public async void AddSubjectToRoutes(ObservableCollection<Model.Information.Subjects> subjects)
        {
            _routes = subjects;
            await saveRouteSubjectDataAsync();
        }
        public async void AddSubjectToEvent(ObservableCollection<Subjects> subjects)
        {
            _events = subjects;
            await saveEventSubjectDataAsync();
        }

        public async void DeleteSubjectFromRoutes(ObservableCollection<Model.Information.Subjects> subjects)
        {
            _routes = subjects;
            await saveRouteSubjectDataAsync();
        }
        public async void DeleteSubjectFromEvents(ObservableCollection<Subjects> subjects)
        {
            _events = subjects;
            await saveEventSubjectDataAsync();
        }

        private async Task saveRouteSubjectDataAsync()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Model.Information.Subjects>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(routefilename,
                CreationCollisionOption.ReplaceExisting))
            {
                jsonSerializer.WriteObject(stream, _routes);
            }
        }
        private async Task saveEventSubjectDataAsync()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Subjects>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(eventfilename,
                CreationCollisionOption.ReplaceExisting))
            {
                jsonSerializer.WriteObject(stream, _events);
            }
        }
    }
}
