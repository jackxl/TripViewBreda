﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.GeoLocation;

namespace TripViewBreda.Navigation
{
    public class RoutePlanner
    {
        public Route GenerateRoute(GPSPoint startpoint, GPSPoint endpoint)
        {
            double startLattitude = startpoint.GetLattitude();
            double startLongitude = startpoint.GetLongitude();
            double endLattitude = endpoint.GetLattitude();
            double endLongitude = endpoint.GetLongitude();
            return null;
        }
    }
}
