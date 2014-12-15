using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace TripViewBreda.GeoLocation
{
    class GPS
    {
        Geopoint myPoint;
        private async void UpdateCurrentLocation()
        {
            var locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 50;

            var position = await locator.GetGeopositionAsync();
            myPoint = position.Coordinate.Point;
        }
        public GPSPoint getCurrentLocation()
        {
            UpdateCurrentLocation();
            return new GPSPoint(myPoint.Position.Latitude, myPoint.Position.Longitude);
        }
    }
}
