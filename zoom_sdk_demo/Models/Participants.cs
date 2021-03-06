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
        public string Notes { get; set; } = "";


    }

    public class ParticipantManager
    {
        public static ObservableCollection<Participant> GetParticipants()
        {
            ObservableCollection<Participant> participants = new ObservableCollection<Participant>();

            participants.Add(new Participant { ID = 1, Name = "Vulpate", Notes=""});
            participants.Add(new Participant { ID = 2, Name = "Mazim", Notes = "" });
            participants.Add(new Participant { ID = 3, Name = "Elit", Notes = "" });
            participants.Add(new Participant { ID = 4, Name = "Etiam", Notes = "" });
            participants.Add(new Participant { ID = 5, Name = "Feugait Eros Libex", Notes = "" });
            participants.Add(new Participant { ID = 6, Name = "Nonummy Erat", Notes = "" });
            participants.Add(new Participant { ID = 7, Name = "Nostrud", Notes = "" });
            participants.Add(new Participant { ID = 8, Name = "Per Modo", Notes = "" });
            participants.Add(new Participant { ID = 9, Name = "Suscipit Ad", Notes = "" });
            participants.Add(new Participant { ID = 10, Name = "Decima"});


            return participants;
        }
    }
}
