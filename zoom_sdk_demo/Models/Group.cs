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
        public bool joinedBO_Room { get; set; } = false;
        public string last_groupID { get; set; } = "";

        //TODO: Substitute for enum, 0 = Hetero, 1 = Homo, 2 = Random
        public int groupType { get; set; } = 2;

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

            // This would put the teacahers and assitants in one group
            this.groupNonStudents();

        }

        private void groupNonStudents()
        {
            var non_students = ParticipantManager.instance.participants.Where(p => !(p.isStudent));
            Group group = new Group();
            group.Name = "Teachers & Assistants";

            foreach (var assistant in non_students)
            {
                group.Participants_in_group.Add(assistant);
            }
            if (group.Participants_in_group.Count != 0) {
                groups.Add(group);
            }

        }
        public void GroupRandom()
        {
            Group group = new Group();
            int count = 0;

            int groupCount = 1;

            var students = ParticipantManager.instance.participants.Where(p => p.isStudent);
            foreach (var participant in students)
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
            for (int i = 0; i < students.Count;)
            {
                // Coppied from above code
                group.Participants_in_group.Add(students[i].Item1);
                i++;

                if (i % groupSize == 0)
                {
                    group.Name = "Room " + groupCount.ToString();
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

            int numGroups = 1 + students.Count / groupSize;

            if (students.Count % groupSize == 0) numGroups--;


            for (int i = 0; i < numGroups; i++)
            {
                groups.Add(new Group());
                groups[i].Name = "Room " + (i + 1);
            }

            for (int i = 0; i < students.Count; i++)
            {
                groups[i % numGroups].Participants_in_group.Add(students[i].Item1);
            }

            //Really dumb code destined to fail
            //Group group = new Group();
            //for (int i = 0; groupCount < numGroups;)
            //{
            //    // Coppied from above code
            //    if ((i * groupSize + groupCount) >= students.Count) break;
            //    group.Participants_in_group.Add(students[(i * groupSize + groupCount)].Item1);
            //    i++;

            //    if (i == groupSize)
            //    {
            //        i = 0;
            //        group.Name = "Group " + (groupCount + 1).ToString();
            //        groups.Add(group);
            //        groupCount++;
            //        if ((i * groupSize + groupCount) < students.Count) group = new Group(); ;

            //        continue;
            //    }

            //}
        }


    }


}
