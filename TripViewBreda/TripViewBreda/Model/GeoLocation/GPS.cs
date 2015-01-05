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
       private async void UpdateCurrentLocation()
        {
            var locator = new Geolocator();
            locator.DesiredAccuracy = PositionAccuracy.High;
            locator.DesiredAccuracyInMeters = 10;

            var position = await locator.GetGeopositionAsync();
            myPoint = position.Coordinate.Point;
        }
        public Geopoint GetCurrentLocation()
        {
            UpdateCurrentLocation();
            return myPoint;
        }
    }
}
