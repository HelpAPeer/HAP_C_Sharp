﻿using System;
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

        public void RemoveParticpant(Array lstUserID)
        {
            //TODO: would be nice to make this code more consist. (smaller and cleaner
            foreach (var participant in participants)
            {
                bool notFound = true;
                for (int i = lstUserID.GetLowerBound(0); i <= lstUserID.GetUpperBound(0); i++)
                {

                    int userid = (int)(UInt32)lstUserID.GetValue(i);
                    if (participant.ID == userid)
                    {
                        // We found a match
                        notFound = false;
                        break;

                    }
                }
                if (notFound)
                {
                    //We have found the item to remove. We can stop the for leap
                    participants.Remove(participant);
                    break;
                }
            }

        }
        public void AddParticipant(Array lstUserID)
        {
            List<int> list_users = new List<int>((IEnumerable<int>)lstUserID);

            //TODO: need to add the ID
            var participant_to_remove = participants.Where(participant => (list_users.Contains(participant.ID)));
            Console.WriteLine(participant_to_remove);

            foreach (var item in participant_to_remove)
            {
                participants.Remove(item);
            }

        }

    }
}
