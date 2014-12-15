using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.GeoLocation
{
    class GPSPoint
    {
        //OVERBODIG????
        //
        //
        private double lattitude;
        private double longitude;

        public GPSPoint(double lattitude, double longitude)
        {
            this.lattitude = lattitude;
            this.longitude = longitude;
        }

        public double GetLattitude()
        {
            return this.lattitude;
        }
        public double GetLongitude()
        {
            return this.longitude;
        }
        
    }
}
