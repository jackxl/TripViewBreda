using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.Model.Information;

namespace TripViewBreda.Model.Routes
{
    public abstract class IRoutePoint
    {
        public abstract Subject GetSubject();
    }
}
