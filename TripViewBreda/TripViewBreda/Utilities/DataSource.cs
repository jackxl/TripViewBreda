using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;

namespace TripViewBreda.Utilities
{
    class DataSource
    {
        private ObservableCollection<Model.Information.Subjects> _routes;
        const string filename = "routes.json";

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

            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Model.Information.Subjects>));

            try
            {
                using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(filename))
                {
                    _routes = (ObservableCollection<Model.Information.Subjects>)jsonSerializer.ReadObject(stream);
                }
            }
            catch (Exception)
            {
                _routes = new ObservableCollection<Model.Information.Subjects>();
            }
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
