using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoom_sdk_demo.Models
{
    // this would contain variables that are genral on the session. 
    // This would be the place dedicated to general varaiables. I fyou need something that needs to cross across windows
    // and doesn't fit other areas

    public sealed class Session
    {
        public static Session instance = new Session();
        private Session() { }
        public bool zoomEmbedded { get; set; } = true;
        public bool firstJoin { get; set; } = true;
        public int total_talking_time = 0;

    }
}
