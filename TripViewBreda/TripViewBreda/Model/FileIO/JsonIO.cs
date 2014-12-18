using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Model.FileIO
{
    class JsonIO : FileIO
    {
        private ObservableCollection<Information.Subjects> subjects;
        private Utilities.DataSource datacontroller;

        public JsonIO(ObservableCollection<Information.Subjects> subjects)
        {
            subjects = new ObservableCollection<Information.Subjects>();
            datacontroller = new Utilities.DataSource();
        }

        public ObservableCollection<Information.Subjects> GetSubjects()
        {
            return subjects;
        }
        
        public async void read()
        {
            subjects = await datacontroller.GetRoutes();
        }

        public bool write(Model.Information.Subjects subject)
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

        public void delete(Model.Information.Subjects subject)
        {
            subjects.Remove(subject);
            datacontroller.DeleteSubject(subjects);
        }
    }
}
