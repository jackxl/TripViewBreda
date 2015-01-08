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
        private Utilities.DataSource datacontroller;

        public JsonIO()
        {
            subjects = new ObservableCollection<Subjects>();
            datacontroller = new Utilities.DataSource();
        }

        public ObservableCollection<Subjects> GetSubjects()
        {
            return subjects;
        }

        public async Task read()
        {
            Debug.WriteLine("Start Reading");
            subjects = await datacontroller.GetRoutes();
            Debug.WriteLine("Done Reading");
        }

        public bool write(Subjects subject)
        {
            try
            {
                subjects.Add(subject);
                datacontroller.AddSubject(subjects);
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
            datacontroller.DeleteSubject(subjects);
        }

        public async Task<Subjects> Find(string name)
        {
            await read();
            Debug.WriteLine("Subjects Size: " + GetSubjects().Count);
            foreach (Subjects route in GetSubjects())
            {
                Debug.WriteLine("Subjects name: " + route.GetName());
                if (route.GetName() == name)
                    return route;
            }
            throw new ArgumentNullException();
            return null;
        }
    }
}
