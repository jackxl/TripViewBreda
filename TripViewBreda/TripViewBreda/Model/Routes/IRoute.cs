using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.Model.Information;
using Windows.Devices.Geolocation;

namespace TripViewBreda.Model.Routes
{
    public abstract class IRoute
    {
        private Subjects subjects = new Subjects();
        public IRoute()
        { InitSubjectPoints(); }
        internal abstract void InitSubjectPoints();
        internal void AddSubject(IRoutePoint point)
        { subjects.AddSubject(point.GetSubject()); }
        public Subjects GetSubjects()
        { return subjects; }
        public void SetSubjects(Subjects subjects)
        { this.subjects = subjects; }
    }
}
