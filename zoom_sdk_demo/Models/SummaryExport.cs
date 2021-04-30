using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace zoom_sdk_demo.Models
{

    class SummaryExport
    {
        // Referencing from the singleton would be better than creating a new instance in the window. 
        // we don't want it tied to a window because it's hard to reference to it. 
        public static SummaryExport instance = new SummaryExport();

        public List<Group[]> groupings = new List<Group[]>();
        public ObservableCollection<Question> questions;


        public string filePath = "MeetingSummary.txt";
        public string sectionDivider = "================================";
        public string entryDivider = "------------------------";
        public string meeting = "Insert meeting title here";
        public string startTime = "";

        //this is private to disallow no more than one instance of this class to be created
        private SummaryExport()
        {
            instance = this;
        }

        public void SetMeetingInfo(string meetingName, DateTime start, ObservableCollection<Question> q)
        {
            meeting = meetingName;
            startTime = start.ToString();
            questions = q;
        }

        public void SaveGroups(ObservableCollection<Group> groups)
        {

            Group[] groupCopies = new Group[groups.Count];
            groups.CopyTo(groupCopies, 0);
            groupings.Add(groupCopies);
        }

        public void WriteSummary()
        {
            string path = Directory.GetCurrentDirectory();

            File.WriteAllText(Path.Combine(path, filePath), GenerateOutput());
        }

        public string GenerateOutput()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine(meeting);
            output.AppendLine(startTime);
            output.AppendLine("\n");

            output.AppendLine(sectionDivider);
            output.AppendLine("Participants List and Notes");
            output.AppendLine(sectionDivider);

            // Participants
            foreach (Participant student in ParticipantManager.instance.participants)
            {
                output.AppendLine(student.Name);
                output.AppendLine("Raised Hand " + student.numberOfTimesRaisedHand.ToString() + " times");

                int contribution_percentage = 0;

                if (Session.instance.total_talking_time != 0)
                    contribution_percentage = (student.TalkTime * 100) / Session.instance.total_talking_time;

                output.AppendLine("Contributed " + contribution_percentage.ToString() + "% of the discussion");
                //output.Append("Evaluation: "); output.AppendLine(student.Evaluation[0].ToString());
                output.AppendLine(student.Notes);
                output.AppendLine();
                output.AppendLine(entryDivider);
            }

            // Saved group configurations
            output.AppendLine(sectionDivider);
            output.AppendLine("Saved Group Configurations");
            output.AppendLine(sectionDivider);
            for (int i = 0; i < groupings.Count; i++)
            {
                output.Append("Group Configuration "); output.AppendLine((i + 1).ToString());
                foreach (Group g in groupings[i])
                {
                    output.Append(g.Name); output.Append(": "); output.AppendLine(String.Join<Participant>(", ", g.Participants_in_group));


                }
                output.AppendLine();
                output.AppendLine(entryDivider);
            }

            // Quiz responses
            output.AppendLine(sectionDivider);
            output.AppendLine("Quiz Results");
            output.AppendLine(sectionDivider);

            // cheeck if questions is null before starting
            if (!(questions is null))
            {
                foreach (Question q in questions)
                {
                    if (!q.used) continue;
                    output.AppendLine(q.question);
                    output.AppendLine(q.imagePath);
                    foreach (KeyValuePair<string, Tuple<string, bool>> response in q.responses)
                    {
                        output.Append(response.Key);
                        output.Append(" \t| ");
                        if (response.Value.Item2)
                        {
                            output.Append("Correct \t| ");
                        }
                        else
                        {
                            output.Append("Incorrect \t| ");
                        }
                        output.AppendLine(response.Value.Item1);
                    }

                    output.AppendLine();
                    output.AppendLine(entryDivider);
                }
            }


            return output.ToString();
        }
    }


}
