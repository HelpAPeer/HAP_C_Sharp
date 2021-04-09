﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace zoom_sdk_demo.Models
{
    public static class GlobalVar
    {
        public const string default_note = "Write your thoughts here on ";
    }

    public class Participant
    {
        public int ID { get; set; }
        public string Name { get; set; } = "";
        //TODO; persoanlize note message. 
        public string Notes { get; set; } = GlobalVar.default_note ;

        // TODO: Distinguish teachers from students
        public bool isStudent = true;

        // Index 0 is always Evaluations from quizzes for now
        public List<double> Evaluation = new List<double>();

        public double Evaluate(ObservableCollection<Question> questions )
        {
            Console.WriteLine("Evaluating student " + Name);

            int numQ = 0;
            double eval = 0;
            foreach (Question q in questions)
            {
                if (q.used && q.responses.ContainsKey(Name))
                {
                    numQ++;
                    if (q.responses[Name].Item2)
                    {
                        eval++;
                    }
                    else
                    {
                        eval--;
                    }
                }
                else if (q.used)
                {
                    Console.WriteLine("Student " + Name + " has not responded to question " + q.question);
                }
                else
                {
                    Console.WriteLine("Question is not eligable");
                }    
            }
            if (numQ > 0)
            {
                if (Evaluation.Count < 1)
                {
                    Evaluation.Add(eval / numQ);
                }
                else
                {
                    Evaluation[0] = eval / numQ;
                }

                Console.WriteLine(Name + " evaluated as " + Evaluation[0]);
                return eval / numQ;
            }
            else
            {
                if (Evaluation.Count < 1)
                {
                    Evaluation.Add(0);
                }
                else
                {
                    Evaluation[0] = 0;
                }
                Console.WriteLine(Name + " not evaluated.");
                return 0;
            }
            
        }


    }

    public sealed class ParticipantManager
    {
        public static ParticipantManager instance = new ParticipantManager();
        private ParticipantManager() { }
        public ObservableCollection<Participant> participants = new ObservableCollection<Participant>();

        // this is for testing
        public void GetParticipants()
        {
            participants.Add(new Participant { ID = 1, Name = "Vulpate" });
            participants.Add(new Participant { ID = 2, Name = "Mazim" });
            participants.Add(new Participant { ID = 3, Name = "Elit" });
            participants.Add(new Participant { ID = 4, Name = "Etiam" });
            participants.Add(new Participant { ID = 5, Name = "Feugait Eros Libex" });
            participants.Add(new Participant { ID = 6, Name = "Nonummy Erat" });
            participants.Add(new Participant { ID = 7, Name = "Nostrud" });
            participants.Add(new Participant { ID = 8, Name = "Per Modo" });
            participants.Add(new Participant { ID = 9, Name = "Suscipit Ad" });
            participants.Add(new Participant { ID = 10, Name = "Decima" });
        }

        public void GetParticipantsInMeeting()
        {
            Array users = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingParticipantsController().GetParticipantsList();

            // Make sure the participant list is empty
            this.participants.Clear();

            for (int i = users.GetLowerBound(0); i <= users.GetUpperBound(0); i++)
            {
                UInt32 userid = (UInt32)users.GetValue(i);
                ZOOM_SDK_DOTNET_WRAP.IUserInfoDotNetWrap user = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                    GetMeetingParticipantsController().GetUserByUserID(userid);

                // Testing the new functionality added

                //Console.WriteLine("Talking");
                //Console.WriteLine(user.IsTalking().ToString());

                if (null != (Object)user)
                {
                    string name = user.GetUserNameW();
                    participants.Add(new Participant { ID = (int)userid, Name = name });
                    Console.Write(userid.ToString());
                    Console.Write(" ");
                    Console.WriteLine(name);
                }
            }
        }

        public void hostChanged(UInt32 userId)
        {
            //check id the new user and their name is the same. Then we have nothing to worry about
            ZOOM_SDK_DOTNET_WRAP.IUserInfoDotNetWrap user = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                  GetMeetingParticipantsController().GetUserByUserID((uint)userId);

            Participant potential_host = participants.Single(i => i.ID == (int)userId);
            Console.WriteLine("Host Changed");
            if (potential_host.Name == user.GetUserNameW())
            {
                Console.WriteLine("Everthing Seems fine. Host ID did not change");
            }


        }

        //This gives the array of participants to remove
        public void RemoveParticpant(Array lstUserID)
        {

            Console.WriteLine("Length of the Users to remove " + lstUserID.Length);

            for (int i = lstUserID.GetLowerBound(0); i <= lstUserID.GetUpperBound(0); i++)
            {
                int userid = (int)(UInt32)lstUserID.GetValue(i);
                Participant participant_toRemove = participants.FirstOrDefault(user => user.ID == (int)userid);

                //TODO: make sure this item is not selected before delete.

                //if no notes were taken on this inidvidual it is safe to delete
                if (!(participant_toRemove is null))
                {
                    if (participant_toRemove.Notes == GlobalVar.default_note)
                    {
                        participants.Remove(participant_toRemove);
                    }

                }

            }
            //foreach (var participant in participants)
            //{
            //    bool notFound = true;
            //    for (int i = lstUserID.GetLowerBound(0); i <= lstUserID.GetUpperBound(0); i++)
            //    {
            //        int userid = (int)(UInt32)lstUserID.GetValue(i);
            //        ZOOM_SDK_DOTNET_WRAP.IUserInfoDotNetWrap user = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
            //       GetMeetingParticipantsController().GetUserByUserID((uint)userid);

            //        if ((participant.ID == userid) & (participant.Name == user.GetUserNameW()))
            //        {
            //            // We found a match
            //            notFound = false;
            //            break;
            //        }

            //    }
            //    if (notFound)
            //    {
            //        //We have found the item to remove. We can stop the for leap
            //        participants.Remove(participant);
            //        break;
            //    }
            //}

        }
        public void ChangeName(UInt32 userId, string userName)
        {
            Console.WriteLine("name was changed");
            Console.WriteLine(userName);
            Participant participant_toModify = participants.Single(user => user.ID == (int)userId);
            participant_toModify.Name = userName;
            int index = participants.IndexOf(participant_toModify);
            participants.RemoveAt(index);
            participants.Insert(index, participant_toModify);
            //participants.Add(new Participant { ID = (int)userId, Name = userName });

        }
        public void AddParticipant(Array lstUserID)
        {
            for (int i = lstUserID.GetLowerBound(0); i <= lstUserID.GetUpperBound(0); i++)
            {
                int userid = (int)(UInt32)lstUserID.GetValue(i);
              
                ZOOM_SDK_DOTNET_WRAP.IUserInfoDotNetWrap user = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
            GetMeetingParticipantsController().GetUserByUserID((uint)userid);
                Console.WriteLine("We found someone  {0}", user.GetUserNameW());
                bool notFound = true;
                foreach (var participant in participants)
                {
                    if ((participant.ID == userid) & (participant.Name == user.GetUserNameW()))
                    {
                        notFound = false;
                        break;
                    }
                }
                if (notFound)
                {
                    //We need add the new participant
                    //ZOOM_SDK_DOTNET_WRAP.IUserInfoDotNetWrap user = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                    //GetMeetingParticipantsController().GetUserByUserID((UInt32)userid);

                    string name = user.GetUserNameW();
                    participants.Add(new Participant { ID = userid, Name = name });
                    break;
                }
            }

        }

        public void EvaluateStudents(ObservableCollection<Question> questions)
        {
            foreach (Participant p in participants)
            {
                if (p.isStudent)
                {
                    p.Evaluate(questions);
                }
            }
        }

        public List<(Participant, double)> GetSortedStudents()
        {
            // Must call Evaluate students first!
            List<(Participant, double)> students = new List<(Participant, double)>();
            foreach (Participant p in participants)
            {
                if (p.isStudent)
                {
                    students.Add((p, p.Evaluation[0]));
                }
            }
            students.Sort(CompareStudents);

            return students;
        }

        public static int CompareStudents((Participant, double) a, (Participant, double) b)
        {
            return a.Item2.CompareTo(b.Item2);
        }
    }
}
