using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Information
{
    class OpenComponent
    {
        private enum Day { Mandag, Dinsdag, Woensdag, Donderdag, Vrijdag, Zaterdag, Zondag };
        private Day day;
        private DateTime openFrom;
        private DateTime openTill;

        public OpenComponent(Day day, DateTime openFrom, DateTime openTill)
        {
            this.day = day;
            this.openFrom = openFrom;
            this.openTill = openTill;
        }

        public Day getDay()
        {
            return day;
        }

        public DateTime getOpenFrom()
        {
            return openFrom;
        }

        public DateTime getOpenTill()
        {
            return openTill;
        }
        public void setDay(Day day)
        {
            this.day = day;
        }

        public void setOpenFrom(DateTime openFrom)
        {
            this.openFrom = openFrom;
        }

        public void setOpenTill(DateTime openTill)
        {
            this.openTill = openTill;
        }
    }
}
