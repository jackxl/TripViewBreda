using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Model.Information
{
    public class Subjects
    {
        private List<Subject> subjects;

        public Subjects()
        {
            subjects = new List<Subject>();
        }

        public List<Subject> GetSubjects()
        {
            return subjects;
        }

        public void AddSubject(Subject subject)
        {
            subjects.Add(subject);
        }

        public void RemoveSubject(Subject subject)
        {
            subjects.Remove(subject);
        }
    }
}
