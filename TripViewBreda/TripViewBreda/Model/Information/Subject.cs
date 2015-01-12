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
        private string imageName;
        private string youtubeVideoID;
        private string name;
        private string information;
        private OpeningHours openingHours;
        private GeoLocation.GPSPoint gPSPoint;

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

        public Subject(GeoLocation.GPSPoint location, string name, string information, string imageName, string youtubeVideoID)
        {
            this.location = location;
            this.name = name;
            this.information = information;
            this.imageName = imageName;
            this.youtubeVideoID = youtubeVideoID;
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
        public string GetImageName()
        {
            return imageName;
        }
        public void SetImageName(string imageName)
        {
            this.imageName = imageName;
        }
        public string GetYoutubeVideoID()
        {
            return youtubeVideoID;
        }
        public void SetYoutubeVideoID(string videoID)
        {
            this.youtubeVideoID = videoID;
        }
        public override string ToString()
        {
            return name + " - " + imageName + ", (" + location.GetLattitude() + ", " + location.GetLongitude() + ") " + information;
        }
    }
}
