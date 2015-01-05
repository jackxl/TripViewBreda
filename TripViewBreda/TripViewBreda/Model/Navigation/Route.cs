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
        private double distance = -1;
        private LinkedList<GPSPoint> routePoints;
        public Route()
        {
            this.routePoints = new LinkedList<GPSPoint>();
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
        public double Distance
        {
            get
            {
                if (distance == -1)
                {
                    distance = 0;
                    GPSPoint[] array = routePoints.ToArray();
                    for (int i = 1; i < routePoints.Count; i++)
                    {
                        GPSPoint p1 = array[i - 1];
                        GPSPoint p2 = array[i];
                        distance += Math.Sqrt(Math.Pow(p1.GetLongitude() - p2.GetLongitude(), 2) + Math.Pow(p1.GetLattitude() - p2.GetLattitude(), 2));
                    }
                }
                return distance;
            }
        }
    }
}
