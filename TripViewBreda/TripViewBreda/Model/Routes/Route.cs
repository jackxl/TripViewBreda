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
        public class HistorischeKM : IRoute
        {
            internal override void InitSubjectPoints()
            {
                AddSubject(new RoutePoint.VVV());
                AddSubject(new RoutePoint.Liefdeszuster());
                AddSubject(new RoutePoint.Nassau_Baronie_Monument());
                AddSubject(new RoutePoint.The_Light_House());
                AddSubject(new RoutePoint.TS_1());
                AddSubject(new RoutePoint.TS_2());
                AddSubject(new RoutePoint.Kasteel_van_Breda());
                AddSubject(new RoutePoint.Stadhouderspoort());
                AddSubject(new RoutePoint.Huis_van_Brecht());
                AddSubject(new RoutePoint.Spanjaardsgat());
                AddSubject(new RoutePoint.Vismarkt());
                AddSubject(new RoutePoint.Havermarkt());
                AddSubject(new RoutePoint.Grote_Kerk());
                AddSubject(new RoutePoint.Het_Poortje());
                AddSubject(new RoutePoint.Ridderstraat());
                AddSubject(new RoutePoint.Grote_Markt());
                AddSubject(new RoutePoint.Bevrijdingsmonument());
                AddSubject(new RoutePoint.Stadhuis());
                AddSubject(new RoutePoint.Antonius_van_Paduakerk());
                AddSubject(new RoutePoint.Bibliotheek());
                AddSubject(new RoutePoint.Kloosterkazerne());
                AddSubject(new RoutePoint.Chasse_theater());
                AddSubject(new RoutePoint.Binding_van_Isaac());
                AddSubject(new RoutePoint.Beyerd());
                AddSubject(new RoutePoint.Gasthuispoort());
                AddSubject(new RoutePoint.Willem_Merkxtuin());
                AddSubject(new RoutePoint.Begijnenhof());
                AddSubject(new RoutePoint.Einde_HistorischeKM());
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
                AddSubject(new RoutePoint.Valkenberg());
            }
        }
        public class Remaining : IRoute
        {
            internal override void InitSubjectPoints()
            {
            }
        }

    }
}
