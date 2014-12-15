using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Navigation
{
    public class Route
    {
        private LinkedList<GPSPoint> RoutePoints { get; set; }
        public Route()
        {
        }
        public void AddNode(GPSPoint node)
        { RoutePoints.AddLast(node); }
        public void RemoveNode(GPSPoint node)
        { RoutePoints.Remove(node); }
    }
}
