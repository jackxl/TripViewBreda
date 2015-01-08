using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.GeoLocation;
using TripViewBreda.Model.Information;

namespace TripViewBreda.Model.Routes
{
    public class RoutePoint
    {
        public class Home : IRoutePoint
        {
            public override Subject GetSubject()
            {
                Subject sub = new Subject(new GPSPoint(51.592342, 4.548881), "Thuis");
                sub.SetImageName("1.jpg");
                return sub;
            }
        }
        public class School : IRoutePoint
        {
            public override Subject GetSubject()
            {
                Subject sub = new Subject(new GPSPoint(51.585477, 4.793091), "School", "Avans Hogeschool info enzo...GG");
                sub.SetImageName("2.jpg");
                return sub;
            }
        }

        public class T_Hart : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.587768, 4.776655), "'t Hart");
            }
        }
        public class Cafe_SamSam : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.587631, 4.776749), "Cafe SamSam");
            }
        }
        public class Studio_Dependance : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.589645, 4.773857), "Studio Dependance");
            }
        }
        public class Millertime : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.588621, 4.77313), "Millertime");
            }
        }
        public class Cafe_Dikke : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.589428, 4.774455), "Café Dikke");
            }
        }

        public class Valkenberg : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.590843, 4.77887), "Valkenberg park");
            }
        }
        public class VVV : IRoutePoint
        {
            public override Subject GetSubject()
            {
                Subject sub = new Subject(new GPSPoint(51.5942879, 4.779192), "VVV");
                sub.SetImageName("2.jpg");
                sub.SetInformation("This is the building of the VVV-building. This is where you also return this phone.");
                return sub;
            }
        }
        public class Liefdeszuster : IRoutePoint
        {
            public override Subject GetSubject()
            {
                Subject sub = new Subject(new GPSPoint(51.593278, 4.779388), "Liefdeszuster");
                sub.SetImageName("3.jpg");
                return sub;
            }
        }
        public class Nassau_Baronie_Monument : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.592500, 4.779695), "Nassau Baronie Monument");
            }
        }
        public class The_Light_House : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.592743, 4.778478), "The Light House");
            }
        }

        public class TS_1 : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.592667, 4.777917), "");
            }
        }

        public class TS_2 : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.591255, 4.777188), "");
            }
        }

        public class Kasteel_van_Breda : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.590515, 4.776223), "Kasteel van Breda");
            }
        }
        public class Stadhouderspoort : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.589695, 4.776138), "Stadhouderspoort");
            }
        }
        public class Huis_van_Brecht : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.590028, 4.774362), "Huis van Brecht (rechter zijde)");
            }
        }
        public class Spanjaardsgat : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.590195, 4.773445), "Spanjaardsgat (rechter zijde)");
            }
        }
        public class Vismarkt : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.589833, 4.773333), "Vismarkt");
            }
        }
        public class Havermarkt : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.589362, 4.774445), "Havermarkt");
            }
        }
        public class Grote_Kerk : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.588833, 4.775278), "Grote Kerk");
            }
        }
        public class Het_Poortje : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.588195, 4.775138), "Het Poortje");
            }
        }
        public class Ridderstraat : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.587083, 4.775750), "Ridderstraat");
            }
        }
        public class Grote_Markt : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.587417, 4.776555), "Grote Markt");
            }
        }
        public class Bevrijdingsmonument : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.588028, 4.776333), "Bevrijdingsmonument");
            }
        }
        public class Stadhuis : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.588750, 4.776112), "Stadhuis");
            }
        }
        public class Antonius_van_Paduakerk : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.587638, 4.777250), "Antonius van Paduakerk");
            }
        }
        public class Bibliotheek : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.588000, 4.778945), "Bibliotheek");
            }
        }
        public class Kloosterkazerne : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.587722, 4.781028), "Kloosterkazerne");
            }
        }
        public class Chasse_theater : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.587750, 4.782000), "Chasse theater");
            }
        }
        public class Binding_van_Isaac : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.588612, 4.780888), "Binding van Isaac");
            }

        }
        public class Beyerd : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.589667, 4.781000), "Beyerd");
            }
        }
        public class Gasthuispoort : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.589555, 4.780000), "Gasthuispoort");
            }
        }
        public class Willem_Merkxtuin : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.589112, 4.777945), "Willem Merkxtuin");
            }
        }
        public class Begijnenhof : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.589695, 4.778362), "Begijnenhof");
            }
        }
        public class Einde_HistorischeKM : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.589500, 4.776250), "Einde stadswandeling");
            }
        }




    }
}
