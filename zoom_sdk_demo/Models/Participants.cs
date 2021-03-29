using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace zoom_sdk_demo.Models
{
    public class Participant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        //TODO; persoanlize note message. 
        public string Notes { get; set; } = "Write your thoughts here on this participant";


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
            //TODO: would be nice to make this code more consist. (smaller and cleaner) This now runs in n2 if the user id is the last one

            Console.WriteLine("Length of the Users to remove " + lstUserID.Length);

            for (int i = lstUserID.GetLowerBound(0); i <= lstUserID.GetUpperBound(0); i++)
            {
                int userid = (int)(UInt32)lstUserID.GetValue(i);
                Participant participant_toRemove = participants.Single(user => user.ID == (int)userid);
                participants.Remove(participant_toRemove);

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
        public void AddParticipant(Array lstUserID)
        {
            for (int i = lstUserID.GetLowerBound(0); i <= lstUserID.GetUpperBound(0); i++)
            {
                int userid = (int)(UInt32)lstUserID.GetValue(i);

                ZOOM_SDK_DOTNET_WRAP.IUserInfoDotNetWrap user = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
            GetMeetingParticipantsController().GetUserByUserID((uint)userid);
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
    }
}
