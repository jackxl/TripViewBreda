using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.GeoLocation;

namespace TripViewBreda.Navigation
{
    public class Route
    {
        private LinkedList<GPSPoint> routePoints;
        public Route()
        {
        }
        public void AddNode(GPSPoint node)
        {
            routePoints.AddLast(node);
        }
        public void RemoveNode(GPSPoint node)
        {
            routePoints.Remove(node);
        }
        public LinkedList<GPSPoint> GetRoutePoints()
        {
            return routePoints;
        }
        public void SetRoutePoints(LinkedList<GPSPoint> routePoints)
        {
            this.routePoints = routePoints;
        }
    }
}
