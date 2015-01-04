using System;
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
            Route route = new Route();
            route.AddNode(startpoint);
            route.AddNode(endpoint);
            return route;
        }
    }
}
