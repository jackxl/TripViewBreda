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
        public static List<string[]> FrequentlyAskedQuestions
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
            // the understanding line is to deside the font size in dependensie with line length
            //list.Add(Topic("1234567890123456789012345678901234567890", "123456789012345678901234567890123456789012345678901234567890"));
            list.Add(Topic("How can i zoom in on the map?", "You have to put two fingers on the screen and stretch them."));
            list.Add(Topic("How can I add a question?", "You have to contact the project group 23TI2A3 at Avans High School @Breda"));
            //list.Add(Topic("Test", "Test"));
            //list.Add(Topic("Test", "Test"));
            //list.Add(Topic("Test", "Test"));
            //list.Add(Topic("Test", "Test"));
            //list.Add(Topic("Test to do a very long line in the topic to see if it wraps at the good height and goes back", "Test description to do the same as in the top. but now in the description to see if it wraps well and did go back. So did it work? I think so other wise this line has allready been removed!\nSo did you like this test? or didn't you?"));
            //list.Add(Topic("Test", "To see if above standing line was wrapped wel"));
            _MushAskedQuestions = list;
        }
        private static string[] Topic(string topicName, string text)
        { return new string[] { topicName, text }; }
    }
}
