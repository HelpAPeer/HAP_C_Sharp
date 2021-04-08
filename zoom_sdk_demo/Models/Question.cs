using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoom_sdk_demo.Models
{
    public class Question
    {
        public String question { get; set; } = "";
        //TODO might need to change this later to an array if a question can have multiple answers
        public String answerString { get; set; } = "";
        public String imagePath { get; set; } = "";

        public bool used { get; set; } = false;

        public List<string> answers;

        public IDictionary<string, (string, bool)> responses;
        public ISet<string> nonresponders;

        // Todo

        public Question()
        {
            nonresponders = new HashSet<string>();
            foreach (Participant p in ParticipantManager.instance.participants)
            {
                nonresponders.Add(p.Name);
            }

            responses = new Dictionary<string, (string, bool)>();
        }

        public void LogResponse(string student, string response)
        {
            var eval = (response, false);
            if (responses.ContainsKey(student))
            {
                Console.WriteLine("Student " + student + " has already responded");
                return;
            }

            if (answers.Contains(response)) //TODO: Add methods for regrading, both for adding more answers and for marking specific student as correct
            {
                eval.Item2 = true;
            }


            responses.Add(student, eval);
            if (nonresponders.Contains(student))
            {
                nonresponders.Remove(student);
            }
        }

        public static Question FromCsv(string csvLine) {
            string[] values = csvLine.Split(',');
            Question q = new Question();
            q.question = values[0];
            q.answerString = values[1];
       
            return q;
        }
    }
}
