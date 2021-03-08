using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoom_sdk_demo.Models
{
    public class Question
    {
        public String question { get; set; } = "";
        //TODO might need to change this later to an array if a question can have multiple answers
        public String answer { get; set; } = "";
        public bool used { get; set; } = false;
    }
}
