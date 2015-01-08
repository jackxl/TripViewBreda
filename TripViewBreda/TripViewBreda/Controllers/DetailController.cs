using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.Model.Information;

namespace TripViewBreda.Controllers
{
    public class DetailController
    {
        private Subject subject;

        public DetailController(Subject subject)
        {
            this.subject = subject;
        }

        public Subject GetSubject()
        {
            return subject;
        }

    }
}
