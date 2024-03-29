﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoom_sdk_demo.Models
{
    class ChatListener
    {
        static string trigger = "==";


        public ChatListener()
        {
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingChatController().Add_CB_onChatMsgNotifcation(onChatMsgNotifcation);

        }

        public void onChatMsgNotifcation(ZOOM_SDK_DOTNET_WRAP.IChatMsgInfoDotNetWrap chatMsg)
        {
            string msg = chatMsg.GetContent();
            string sdr = chatMsg.GetSenderDisplayName();
            // bool all = chatMsg.IsChatToAll;

            Console.WriteLine("Message recieved: " + msg);

            if (msg.StartsWith(trigger))
            {
                //this might be causing problems
                if (QuestionManager.instance.activeQuestion == null)
                {
                    Console.WriteLine("Question not active");
                }
                else
                {
                    string response = msg.Trim().Substring(2).Trim().ToLowerInvariant();
                    Console.WriteLine("Logging response to question-" + response);

                    QuestionManager.instance.activeQuestion.LogResponse(sdr, response);
                    //HAP_MainWindow.activeQuestion.LogResponse(sdr, response);
                }
            }

        }


    }
}
