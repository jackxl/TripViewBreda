using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Model.Information
{
    public class OpenComponent
    {
        public enum Day { Maandag, Dinsdag, Woensdag, Donderdag, Vrijdag, Zaterdag, Zondag };
        private Day day;
        private DateTime openFrom;
        private DateTime openTill;

        public OpenComponent(Day day, DateTime openFrom, DateTime openTill)
        {
            this.day = day;
            this.openFrom = openFrom;
            this.openTill = openTill;
        }

        public Day GetDay()
        {
            return day;
        }

        public void SetDay(Day day)
        {
            this.day = day;
        }

        public DateTime GetOpenFrom()
        {
            return openFrom;
        }

        public void SetOpenFrom(DateTime openFrom)
        {
            this.openFrom = openFrom;
        }

        public DateTime GetOpenTill()
        {
            return openTill;
        }

        public void SetOpenTill(DateTime openTill)
        {
            this.openTill = openTill;
        }
        public static Day GetDay(int dayOfTheWeek)
        {
            return (Day)(dayOfTheWeek);
        }
        public override string ToString()
        {
            return day + " - [" + openFrom + " - " + openTill + "]";
        }
    }
}
