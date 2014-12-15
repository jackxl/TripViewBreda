using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.GeoLocation
{
    public class GPSPoint
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
        /// <summary>
        /// Description: 
        /// This is the vertical line around the earth.
        /// </summary>
        public double GetLattitude()
        {
            return lattitude;
        }

        /// <summary>
        /// Description:
        /// This is the horizontal line around the earth.
        /// </summary>
        public double GetLongitude()
        {
            return this.longitude;
        }

    }
}
