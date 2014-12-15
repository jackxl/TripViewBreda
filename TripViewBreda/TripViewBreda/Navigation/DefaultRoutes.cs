using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Navigation
{
    public class DefaultRoutes
    {
        public List<Route> Routes { get; private set; }
        public DefaultRoutes()
        {
            this.Routes = new List<Route>();
        }
    }
}
