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
    class DataModel
    {
        public class MapNote
        {
            public string Title { get; set; }
            public string Note { get; set; }
            public DateTime Create { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        public class DataSource
        {
            private ObservableCollection<MapNote> _mapnotes;
            const string filename = "mapnotes.json";

            public DataSource()
            {
                _mapnotes = new ObservableCollection<MapNote>();
            }

            public async Task<ObservableCollection<MapNote>> GetMapNotes()
            {
                await ensureDataLoaded();
                return _mapnotes;
            }

            private async Task ensureDataLoaded()
            {
                if (_mapnotes.Count == 0)
                    await getMapNoteDataAsync();

                return;
            }

            private async Task getMapNoteDataAsync()
            {
                if (_mapnotes.Count != 0)
                    return;

                var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<MapNote>));

                try
                {
                    // Add a using System.IO
                    using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(filename))
                    {
                        _mapnotes = (ObservableCollection<MapNote>)jsonSerializer.ReadObject(stream);
                    }
                }
                catch (Exception)
                {
                    _mapnotes = new ObservableCollection<MapNote>();
                }
            }

            public async void AddMapNote(MapNote mapNote)
            {
                _mapnotes.Add(mapNote);
                await saveMapNoteDataAsync();
            }

            private async Task saveMapNoteDataAsync()
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<MapNote>));
                using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(filename,
                    CreationCollisionOption.ReplaceExisting))
                {
                    jsonSerializer.WriteObject(stream, _mapnotes);
                }
            }

            public async void DeleteMapNote(MapNote mapNote)
            {
                _mapnotes.Remove(mapNote);
                await saveMapNoteDataAsync();
            }
        }
    }
}
