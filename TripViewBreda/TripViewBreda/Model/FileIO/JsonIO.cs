using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripViewBreda.Model.Information;

namespace TripViewBreda.Model.FileIO
{
    public class JsonIO : FileIO
    {
        private ObservableCollection<Subjects> subjects;
        private ObservableCollection<Subjects> events;
        private Utilities.DataSource datacontroller;

        public JsonIO()
        {
            subjects = new ObservableCollection<Subjects>();
            events = new ObservableCollection<Subjects>();
            datacontroller = new Utilities.DataSource();
        }

        public ObservableCollection<Subjects> GetSubjects()
        {
            return subjects;
        }
        public ObservableCollection<Subjects> GetEvents()
        {
            return events;
        }

        public async Task read()
        {
            subjects = await datacontroller.GetRoutes();
            events = await datacontroller.GetEvents();
        }

        public bool write(Subjects subject)
        {
            try
            {
                subjects.Add(subject);
                datacontroller.AddSubjectToRoutes(subjects);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool writeEvent(Subjects subject)
        {
            try
            {
                events.Add(subject);
                datacontroller.AddSubjectToEvent(events);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void delete(Subjects subject)
        {
            subjects.Remove(subject);
            datacontroller.DeleteSubjectFromRoutes(subjects);
        }
        public void deleteEvent(Subjects subject)
        {
            events.Remove(subject);
            datacontroller.DeleteSubjectFromEvents(events);
        }

        public async Task<Subjects> Find(string name)
        {
            await read();
            foreach (Subjects route in GetSubjects())
            {
                if (route.GetName() == name)
                    return route;
            }
            throw new ArgumentNullException();
            return null;
        }
    }
}
