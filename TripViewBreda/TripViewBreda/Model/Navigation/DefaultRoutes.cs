using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Navigation
{
    public class DefaultRoutes
    {
        private List<Route> routes;
        public DefaultRoutes()
        {
            this.routes = new List<Route>();
        }
        public List<Route> GetRoutes()
        {
            return routes;
        }
    }
}
