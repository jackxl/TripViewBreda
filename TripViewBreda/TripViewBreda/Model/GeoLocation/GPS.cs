using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace TripViewBreda.GeoLocation
{
    public class GPS
    {
       private Geopoint myPoint;
       private async Task UpdateCurrentLocation()
        {
            var locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 50;

            var position = await locator.GetGeopositionAsync();
            myPoint = position.Coordinate.Point;
        }
        public async Task<Geopoint> GetCurrentLocation()
        {
            await UpdateCurrentLocation();
            return myPoint;
        }
    }
}
