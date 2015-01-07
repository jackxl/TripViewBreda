using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.GeoLocation;
using TripViewBreda.Model.Information;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TripViewBreda.Model.Routes
{
    public class Route
    {
        public class School : IRoute
        {
            internal override void InitSubjectPoints()
            {
                AddSubject(new RoutePoint.School());
            }
        }
        public class Home : IRoute
        {
            internal override void InitSubjectPoints()
            {
                AddSubject(new RoutePoint.Home());
            }
        }
        public class Cafes : IRoute
        {
            internal override void InitSubjectPoints()
            {
                AddSubject(new RoutePoint.T_Hart());
                AddSubject(new RoutePoint.Cafe_SamSam());
                AddSubject(new RoutePoint.Studio_Dependance());
                AddSubject(new RoutePoint.Millertime());
            }
        }
        public class Tourist_Trail : IRoute
        {
            internal override void InitSubjectPoints()
            {
                throw new NotImplementedException();
            }
        }
        public class Remaining : IRoute
        {
            internal override void InitSubjectPoints()
            {
                throw new NotImplementedException();
            }
        }

    }
}
