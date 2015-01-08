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

namespace TripViewBreda.Utilities
{
    class DataSource
    {
        private ObservableCollection<Model.Information.Subjects> _routes;
        const string filename = "ms-appx:///routes.json";

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
            //if (_routes.Count != 0)
            //    return;

            //var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Model.Information.Subjects>));

            //try
            //{
            //    using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(filename))
            //    {
            //        _routes = (ObservableCollection<Model.Information.Subjects>)jsonSerializer.ReadObject(stream);
            //    }
            //}
            //catch (Exception)
            //{
            //    _routes = new ObservableCollection<Model.Information.Subjects>();
            //}

            if (_routes.Count != 0)
                return;

            try
            {
                Uri appUri = new Uri(filename);
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(appUri);
                string jsonText = await FileIO.ReadTextAsync(file);
                JsonObject jsonObject = JsonObject.Parse(jsonText);
                JsonArray jsonArray = jsonObject["subjects"].GetArray();
                foreach (JsonValue route in jsonArray)
                {
                    Model.Information.Subjects subjects = new Model.Information.Subjects();
                    JsonObject routeObject = route.GetObject();

                    //List<Model.Information.Subject> subjects = new List<Model.Information.Subject>();
                    foreach (JsonValue subject in routeObject["Tasks"].GetArray())
                    {
                        JsonObject subjectObject = subject.GetObject();
                        subjects.AddSubject(new Model.Information.Subject((GeoLocation.GPSPoint)subjectObject["location"],
                                                         subjectObject["name"].GetString(),
                                                         subjectObject["information"].GetString(),
                                                         subjectObject["imageName"].GetString()));
                    }
                    _routes.Add(subjects);
                }
            }
            catch (Exception)
            {

                _routes = new ObservableCollection<Model.Information.Subjects>();
            }

            //Uri appUri = new Uri(filename);
            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(appUri);
            //string jsonText = await FileIO.ReadTextAsync(file);
            //JsonObject jsonObject = JsonObject.Parse(jsonText);
            //JsonArray jsonArray = jsonObject["subjects"].GetArray();
            //foreach (JsonValue route in jsonArray)
            //{
            //    Model.Information.Subjects subjects = new Model.Information.Subjects();
            //    JsonObject routeObject = route.GetObject();

            //    //List<Model.Information.Subject> subjects = new List<Model.Information.Subject>();
            //    foreach (JsonValue subject in routeObject["Tasks"].GetArray())
            //    {
            //        JsonObject subjectObject = subject.GetObject();
            //        subjects.AddSubject(new Model.Information.Subject((GeoLocation.GPSPoint)subjectObject["location"],
            //                                         subjectObject["name"].GetString(),
            //                                         subjectObject["information"].GetString()));
            //    }
            //    _routes.Add(subjects);
            //}
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
