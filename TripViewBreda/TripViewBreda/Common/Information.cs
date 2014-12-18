using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripViewBreda.Common
{
    public class Information
    {
        private static List<string[]> _MushAskedQuestions = null;
        private Information() { }
        public static List<string[]> MuchAskedQuestions
        {
            get
            {
                if (_MushAskedQuestions == null)
                    CreateMushAskedQuestionsList();
                return _MushAskedQuestions;
            }
        }
        private static void CreateMushAskedQuestionsList()
        {
            List<string[]> list = new List<string[]>();
            list.Add(Topic("How does this work?", "you found out yourself!"));
            list.Add(Topic("How can i zoom in on the map?", "You have to put two fingers on the screen and stretch them."));
            _MushAskedQuestions = list;
        }
        private static string[] Topic(string topicName, string text)
        { return new string[] { topicName, text }; }
    }
}
