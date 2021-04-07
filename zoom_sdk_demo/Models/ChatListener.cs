using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoom_sdk_demo.Models
{
    class ChatListener
    {
        static string trigger = "!~";


        public ChatListener()
        {
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingChatController().Add_CB_onChatMsgNotifcation(onChatMsgNotifcation);

        }

        public void onChatMsgNotifcation(ZOOM_SDK_DOTNET_WRAP.IChatMsgInfoDotNetWrap chatMsg)
        {
            string msg = chatMsg.GetContent();
            string sdr = chatMsg.GetSenderDisplayName();

            Console.WriteLine("Message recieved: " + msg);

            if (msg.StartsWith(trigger))
            {
                if (HAP_MainWindow.activeQuestion == null)
                {
                    Console.WriteLine("Question not active");
                }
                else
                {
                    Console.WriteLine("Logging response to question-" + msg.Substring(2).Trim());
                    HAP_MainWindow.activeQuestion.LogResponse(sdr, msg.Substring(2).Trim());
                }
            }

        }


    }
}
