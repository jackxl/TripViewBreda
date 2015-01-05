using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Model.Information
{
    public class Subject
    {
        public GeoLocation.GPSPoint location;
        private string name;
        private string information;
        private OpeningHours openingHours;

        public Subject(GeoLocation.GPSPoint location, string name)
        {
            this.location = location;
            this.name = name;
            information = string.Empty;
            openingHours = new OpeningHours();
        }

        public Subject(GeoLocation.GPSPoint location, string name, string information)
        {
            this.location = location;
            this.name = name;
            this.information = information;
            openingHours = new OpeningHours();
        }

        public Subject(GeoLocation.GPSPoint location, string name, OpeningHours openingHours)
        {
            this.location = location;
            this.name = name;
            information = string.Empty;
            this.openingHours = openingHours;
        }

        public GeoLocation.GPSPoint GetLocation()
        {
            return location;
        }

        public void SetLocation(GeoLocation.GPSPoint location)
        {
            this.location = location;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetInformation()
        {
            return information;
        }

        public void SetInformation(string information)
        {
            this.information = information;
        }

        public OpeningHours GetOpeningHours()
        {
            return openingHours;
        }

        public void SetOpeningsHours(OpeningHours openingHours)
        {
            this.openingHours = openingHours;
        }
    }
}
