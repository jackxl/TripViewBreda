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
                return new Subject(new GPSPoint(51.592342, 4.548881), "Thuis");
            }
        }
        public class School : IRoutePoint
        {
            public override Subject GetSubject()
            {
                return new Subject(new GPSPoint(51.585477, 4.793091), "School", "Avans Hogeschool info enzo...GG");
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
    }
}
