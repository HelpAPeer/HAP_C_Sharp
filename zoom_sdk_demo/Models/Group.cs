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
        public String group_ID { get; set; } = "";

    }

    public sealed class GroupManager
    {
        public static GroupManager instance = new GroupManager();
        private GroupManager() { }
        public ObservableCollection<Group> groups = new ObservableCollection<Group>();
        public int groupSize { get; set; } = 3;

        // Substitute for enum, 0 = Hetero, 1 = Homo, 2 = Random
        public int groupType { get; set; } = 0;

        public void getGroups()
        {
            //We need to get groups from particpant manager
            if (ParticipantManager.instance.participants.Count < 1)
            {
                ParticipantManager.instance.GetParticipants();
            }
            groups.Clear();

            if (groupType == 2)
            {
                GroupRandom();
            }
            else
            {
                if (groupType == 1)
                {
                    GroupHomogeneous();
                }
                else
                {
                    GroupHeterogeneous();
                }
            }

        }

        public void GroupRandom()
        {
            Group group = new Group();
            int count = 0;

            int groupCount = 1;
            foreach (var participant in ParticipantManager.instance.participants)
            {
                group.Participants_in_group.Add(participant);
                count++;
                if (count % groupSize == 0)
                {
                    group.Name = "Room " + groupCount.ToString();
                    groups.Add(group);
                    groupCount++;
                    group = new Group();
                    continue;
                }
                //we neeed to make sure we we are in the last count. the rest will hold the remainder of the group.
                //Continue for the if before holds if we have exactly number of participants divisble by group size
                if (count == ParticipantManager.instance.participants.Count)
                {
                    group.Name = "Room " + groupCount.ToString();
                    groups.Add(group);

                }
            }
        }

        public void GroupHomogeneous()
        {
            var students = ParticipantManager.instance.GetSortedStudents();

            int groupCount = 1;
            Group group = new Group();
            for (int i = 0; i < students.Count; )
            {
                // Coppied from above code
                group.Participants_in_group.Add(students[i].Item1);
                i++;

                if (i % groupSize == 0)
                {
                    group.Name = "Group " + groupCount.ToString();
                    groups.Add(group);
                    groupCount++;
                    group = new Group();
                    continue;
                }
                //we neeed to make sure we we are in the last count. the rest will hold the remainder of the group.
                //Continue for the if before holds if we have exactly number of participants divisble by group size
                if (i == students.Count)
                {
                    group.Name = "Group " + groupCount.ToString();
                    groups.Add(group);

                }
            }
        }

        public void GroupHeterogeneous()
        {
            var students = ParticipantManager.instance.GetSortedStudents();
            int numGroups = students.Count / groupSize;
            if (students.Count % groupSize != 0) numGroups++;

            int groupCount = 0;
            Group group = new Group();
            for (int i = 0; groupCount < numGroups;)
            {
                // Coppied from above code
                if ((i * groupSize + groupCount) >= students.Count) break;
                group.Participants_in_group.Add(students[(i * groupSize + groupCount)].Item1);
                i++;

                if (i == groupSize)
                {
                    i = 0;
                    group.Name = "Group " + (groupCount + 1).ToString();
                    groups.Add(group);
                    groupCount++;
                    group = new Group();
                    continue;
                }

            }

            group.Name = "Group " + (groupCount + 1).ToString();
            groups.Add(group);

        }


    }


}
