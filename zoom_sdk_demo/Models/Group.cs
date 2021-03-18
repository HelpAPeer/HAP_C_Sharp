using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoom_sdk_demo.Models
{
    public class Group
    {
        public String Name { get; set; } = "";
        public ObservableCollection<Participant> Participants_in_group { get; set; } = new ObservableCollection<Participant>();

    }

    public sealed class GroupManager
    {
        public static GroupManager instance = new GroupManager();
        private GroupManager() { }
        public ObservableCollection<Group> groups = new ObservableCollection<Group>();
        public int groupSize { get; set; } = 3;

        public void getGroups()
        {
            //We need to get groups from particpant manager
            if (ParticipantManager.instance.participants.Count < 1)
            {
                ParticipantManager.instance.GetParticipants();
            }
            groups.Clear();
            Group group = new Group();
            int count = 0;
            
            int groupCount = 1;
            foreach (var participant in ParticipantManager.instance.participants)
            {
                group.Participants_in_group.Add(participant);
                count++;
                if (count % groupSize == 0)
                {
                    group.Name = "Group " + groupCount.ToString();
                    groups.Add(group);
                    groupCount++;
                    group = new Group();
                    continue;
                }
                //we neeed to make sure we we are in the last count. the rest will hold the remainder of the group.
                //Continue for the if before holds if we have exactly number of participants divisble by group size
                if (count == ParticipantManager.instance.participants.Count)
                {
                    group.Name = "Group " + groupCount.ToString();
                    groups.Add(group);

                }
            }
        }
    }


}
