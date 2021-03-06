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
        public string Notes { get; set; } = "Write your thoughts here on this participant" ;


    }

    public class ParticipantManager
    {
        public static ObservableCollection<Participant> GetParticipants()
        {
            ObservableCollection<Participant> participants = new ObservableCollection<Participant>();

            participants.Add(new Participant { ID = 1, Name = "Vulpate"});
            participants.Add(new Participant { ID = 2, Name = "Mazim"});
            participants.Add(new Participant { ID = 3, Name = "Elit"});
            participants.Add(new Participant { ID = 4, Name = "Etiam"});
            participants.Add(new Participant { ID = 5, Name = "Feugait Eros Libex"});
            participants.Add(new Participant { ID = 6, Name = "Nonummy Erat"});
            participants.Add(new Participant { ID = 7, Name = "Nostrud"});
            participants.Add(new Participant { ID = 8, Name = "Per Modo"});
            participants.Add(new Participant { ID = 9, Name = "Suscipit Ad"});
            participants.Add(new Participant { ID = 10, Name = "Decima"});


            return participants;
        }
    }
}
