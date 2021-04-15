using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public List<string> answers { get; set; } = new List<string>();

        public IDictionary<string, Tuple<string, bool>> responses { get; set; } = new Dictionary<string, Tuple<string, bool>>();
        public ISet<string> nonresponders { get; set; }

        // Todo

        //This isn't alwsys called as such
        public Question()
        {
            nonresponders = new HashSet<string>();
            foreach (Participant p in ParticipantManager.instance.participants)
            {
                nonresponders.Add(p.Name);
            }

            responses = new Dictionary<string, Tuple<string, bool>>();
            answers = new List<string>();
        }

        public void LogResponse(string student, string response)
        {
            Console.WriteLine("We are loggin response");

            if (responses.ContainsKey(student))
            {
                Console.WriteLine("Student " + student + " has already responded");
                return;
            }

            Tuple<string, bool> eval;
            if (answers.Contains(response)) //TODO: Add methods for regrading, both for adding more answers and for marking specific student as correct
            {
                eval = Tuple.Create(response, true);
            }
            else
            {
                eval = Tuple.Create(response, false);
            }


            responses.Add(student, eval);
            if (nonresponders.Contains(student))
            {
                nonresponders.Remove(student);
            }
        }

        public static Question FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Question q = new Question();

            //Check if question is null

            q.question = values[0];

            q.answerString = values[1];

            return q;
        }
    }


    //Created a quesiton maanger
    public sealed class QuestionManager
    {
        public static QuestionManager instance = new QuestionManager();
        private QuestionManager() { }
        public ObservableCollection<Question> questions = new ObservableCollection<Question>();
        public Question activeQuestion = null;
    }
}
