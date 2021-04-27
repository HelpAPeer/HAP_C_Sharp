using System;
using System.ComponentModel; // CancelEventArgs
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using zoom_sdk_demo.Models;
using ZOOM_SDK_DOTNET_WRAP;

namespace zoom_sdk_demo
{
    /// <summary>
    /// Interaction logic for start_join_meeting.xaml
    /// </summary>
    public partial class start_join_meeting : Window
    {
        HAP_MainWindow hAP_MainWindow = new HAP_MainWindow();
        private Timer timer1;
        private int loop = 0;

        public start_join_meeting()
        {
            InitializeComponent();
            Random random = new Random();  // Only do this once
            //string[] images = Directory.GetFiles(@".\Assets\SlideShow\", "*.*");
            //string[] images = Directory.GetFiles(@"ms-appx:///Assets/SlideShow/", "*.*");
            InitTimer();

        }

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 2000; // in miliseconds
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] images = { @"/Assets/Slideshow/HelpAPeer_Poster.png", @"/Assets/Slideshow/HelpAPeer_Logo.jpg" };
            int index = loop % images.Length;
            SlideShow.Source = new BitmapImage(new Uri(images[index], UriKind.Relative));
            loop++;
        }

        //ZOOM_SDK_DOTNET_WRAP.onMeetingStatusChanged
        public void onMeetingStatusChanged(MeetingStatus status, int iResult)
        {

            switch (status)
            {
                case ZOOM_SDK_DOTNET_WRAP.MeetingStatus.MEETING_STATUS_ENDED:
                    {
                        hAP_MainWindow.Hide();
                        //ParticipantManager.instance.participants.Clear();
                        System.Windows.Application.Current.Shutdown();

                    }
                    break;
                case ZOOM_SDK_DOTNET_WRAP.MeetingStatus.MEETING_STATUS_FAILED:
                    {
                        //TODO: need to check if window is visible first before performing hide. Might be damaging
                        hAP_MainWindow.Hide();
                        ParticipantManager.instance.participants.Clear();

                        Show();
                    }
                    break;
                //This is called when you join meeting and when you join a breakout room as well
                case ZOOM_SDK_DOTNET_WRAP.MeetingStatus.MEETING_STATUS_INMEETING:
                    {


                        // Load the meeting partipants now
                        // Would be best to update the observable list. Instead of tying a new one https://gist.github.com/tymorrow/9397870

                        //Check that we aren't join a breakout room! We don't want to get the new part
                        if (Session.instance.firstJoin)
                        {
                            //SummaryExport.instance.SetMeetingInfo(CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingInfo().GetInviteEmailTitle(), DateTime.Now, QuestionManager.instance.questions);
                            SummaryExport.instance.SetMeetingInfo(CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingInfo().GetMeetingTopic(), DateTime.Now, QuestionManager.instance.questions);
                            Session.instance.firstJoin = false;
                        }

                        if (!GroupManager.instance.joinedBO_Room)
                        {
                            ParticipantManager.instance.GetParticipantsInMeeting();
                        }
                        //would be best to show the Ui When we are in the meeting here. This is the view we actually care about
                        hAP_MainWindow.Show();
                        hAP_MainWindow.embedZoom();
                    }
                    break;
                case MeetingStatus.MEETING_STATUS_LEAVE_BREAKOUT_ROOM:
                    {
                        //We left the breakout room. we would need to add some functions into this
                        GroupManager.instance.joinedBO_Room = false;
                    }
                    break;


                default://todo
                    break;
            }
        }

        //Callback event of notification of users who are in the meeting.
        public void onUserJoin(Array lstUserID)
        {
            Console.WriteLine("participant Joined");
            //TODO: need a way to rest if in main room
            if (!(GroupManager.instance.joinedBO_Room))
            {
                ParticipantManager.instance.AddParticipant(lstUserID);
            }

        }

        //Callback event of notification of user who leaves the meeting.
        //Parameters
        //lstUserID List of the user ID who leaves the meeting.
        public void onUserLeft(Array lstUserID)
        {
            //TODO: fix the remove particpnat code. It is causing some issues with the oneselect UI
            ParticipantManager.instance.RemoveParticpant(lstUserID);
        }

        //userId	Specify the ID of the new host.

        public void onHostChangeNotification(UInt32 userId)
        {
            // might need to readdress the issue here.[IDs don't change when hosts change]
            ParticipantManager.instance.hostChanged(userId);
        }

        public void onCoHostChangeNotification(UInt32 userId, bool isCoHost)
        {
            Console.WriteLine("Co-Host was changed.{0} {1}", userId, isCoHost);
            ParticipantManager.instance.coHostChanged(userId, isCoHost);

        }
        public void onLowOrRaiseHandStatusChanged(bool bLow, UInt32 userid)
        {
            Console.WriteLine("SomeOne Raised Hand id:{0} handUp {1}", userid, bLow);
            ParticipantManager.instance.raiseHandCounter(bLow, userid);
        }

        public void onUserNameChanged(UInt32 userId, string userName)
        {
            ParticipantManager.instance.ChangeName(userId, userName);

            //Console.WriteLine(userName);
            Console.WriteLine("{0} {1}", userId, userName);
            Array users = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingParticipantsController().GetParticipantsList();
            //Console.WriteLine(String.Join("\n", users));
            Console.WriteLine("List of Users Currently");
            foreach (var item in users)
            {
                Console.WriteLine(item.ToString());
            }
        }
        private void RegisterCallBack()
        {
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Add_CB_onMeetingStatusChanged(onMeetingStatusChanged);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onHostChangeNotification(onHostChangeNotification);

            // add the onCoHostChangeNotification. this was added to the wrapper in order for this to work.
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
               GetMeetingParticipantsController().Add_CB_onCoHostChangeNotification(onCoHostChangeNotification);



            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
     GetMeetingParticipantsController().Add_CB_onLowOrRaiseHandStatusChanged(onLowOrRaiseHandStatusChanged);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onUserJoin(onUserJoin);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onUserLeft(onUserLeft);
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().
                GetMeetingParticipantsController().Add_CB_onUserNameChanged(onUserNameChanged);
            //CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingParticipantsController().
        }
        private void button_start_api_Click(object sender, RoutedEventArgs e)
        {
            RegisterCallBack();
            ZOOM_SDK_DOTNET_WRAP.StartParam param = new ZOOM_SDK_DOTNET_WRAP.StartParam();

            //param.userType = ZOOM_SDK_DOTNET_WRAP.SDKUserType.SDK_UT_WITHOUT_LOGIN;
            //ZOOM_SDK_DOTNET_WRAP.StartParam4WithoutLogin start_withoutlogin_param = new ZOOM_SDK_DOTNET_WRAP.StartParam4WithoutLogin();
            //start_withoutlogin_param.meetingNumber = UInt64.Parse(textBox_meetingnumber_api.Text);
            //start_withoutlogin_param.userID = textBox_userid_api.Text;
            ////start_withoutlogin_param.userZAK = textBox_AccessToken.Text;
            //start_withoutlogin_param.userName = textBox_username_api.Text;
            //start_withoutlogin_param.zoomuserType = ZOOM_SDK_DOTNET_WRAP.ZoomUserType.ZoomUserType_APIUSER;
            //param.withoutloginStart = start_withoutlogin_param;

            param.userType = SDKUserType.SDK_UT_NORMALUSER;
            ZOOM_SDK_DOTNET_WRAP.StartParam4NormalUser start_withlogin = new ZOOM_SDK_DOTNET_WRAP.StartParam4NormalUser();
            //start_withlogin.
            start_withlogin.meetingNumber = UInt64.Parse(textBox_meetingnumber_api.Text.Replace(" ", ""));

            param.normaluserStart = start_withlogin;

            ZOOM_SDK_DOTNET_WRAP.SDKError err = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Start(param);
            if (ZOOM_SDK_DOTNET_WRAP.SDKError.SDKERR_SUCCESS == err)
            {
                Hide();
            }
            else//error handle
            { }

        }

        private void button_join_api_Click(object sender, RoutedEventArgs e)
        {
            RegisterCallBack();
            ZOOM_SDK_DOTNET_WRAP.JoinParam param = new ZOOM_SDK_DOTNET_WRAP.JoinParam();

            //param.userType = ZOOM_SDK_DOTNET_WRAP.SDKUserType.SDK_UT_WITHOUT_LOGIN;
            //ZOOM_SDK_DOTNET_WRAP.JoinParam4WithoutLogin join_api_param = new ZOOM_SDK_DOTNET_WRAP.JoinParam4WithoutLogin();
            //// remove the api.text empty spaces
            //join_api_param.meetingNumber = UInt64.Parse(textBox_meetingnumber_api.Text.Replace(" ", ""));
            //join_api_param.userName = textBox_username_api.Text;
            //param.withoutloginJoin = join_api_param;

            param.userType = ZOOM_SDK_DOTNET_WRAP.SDKUserType.SDK_UT_NORMALUSER;
            ZOOM_SDK_DOTNET_WRAP.JoinParam4NormalUser join_api_param = new ZOOM_SDK_DOTNET_WRAP.JoinParam4NormalUser();
            join_api_param.meetingNumber = UInt64.Parse(textBox_meetingnumber_api.Text.Replace(" ", ""));
            join_api_param.psw = textBox_meetingPass_api.Password;
            param.normaluserJoin = join_api_param;


            ZOOM_SDK_DOTNET_WRAP.SDKError err = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Join(param);

            if (ZOOM_SDK_DOTNET_WRAP.SDKError.SDKERR_SUCCESS == err)
            {
                Hide();

            }
            else//error handle
            { }
        }

        void Wnd_Closing(object sender, CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void textBox_meetingPass_api_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void QuizSetting_Click(object sender, RoutedEventArgs e)
        {
            var addQuestionWindow = new AddQuestionWindow();
            addQuestionWindow.ShowDialog();
        }

        private void openWebpageClick(object sender, RoutedEventArgs e)
        {
            string link_to_details = "https://projects.etc.cmu.edu/help-a-peer/?page_id=467";
            Process p = Process.Start(link_to_details);

        }
    }
}
