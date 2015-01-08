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
        private string name;

        public Subjects()
        {
            subjects = new List<Subject>();
        }

        public Subjects(string name)
        {
            subjects = new List<Subject>();
            this.name = name;
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

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }
    }
}
