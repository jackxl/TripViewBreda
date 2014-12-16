using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace TripViewBreda.Utilities
{
    public class StorageController
    {
        private static StorageController _instance = null ;
        private SemaphoreSlim _syncLock = new SemaphoreSlim(1);

        private List<int> completedCachesBuffer;

        public static StorageController Instance() 
        {
            if (_instance == null)
                _instance = new StorageController();

            return _instance;
        }

        private StorageController() 
        {
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<List<int>> readHotspots() 
        {
            await _syncLock.WaitAsync();
            StorageFolder folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var file = await folder.OpenStreamForReadAsync("hotspots.txt");
            string val;
            // Read the data.
            using (StreamReader streamReader = new StreamReader(file))
            {
                val = streamReader.ReadToEnd();
            }

            List<int> result = new List<int>();
            result = (List<int>)JsonConvert.DeserializeObject(val, result.GetType());
            _syncLock.Release();
            return result;
        }


        public async Task<int> saveFoundCaches(List<int> found) 
        {
            await _syncLock.WaitAsync();
            string myJson = JsonConvert.SerializeObject(found);
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(myJson.ToCharArray());

            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            var file = await local.CreateFileAsync("foundCaches.txt",
            CreationCollisionOption.ReplaceExisting);

            using (var s = await file.OpenStreamForWriteAsync())
            {
                try
                {
                    s.Write(fileBytes, 0, fileBytes.Length);
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
            _syncLock.Release();
            return 1;
        }
    }
}
