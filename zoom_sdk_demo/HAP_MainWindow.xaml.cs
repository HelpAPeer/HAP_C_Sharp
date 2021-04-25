using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using zoom_sdk_demo.Models;
using ZOOM_SDK_DOTNET_WRAP;

namespace zoom_sdk_demo
{
    /// <summary>
    /// Interaction logic for HAP_MainWindow.xaml
    /// </summary>
    /// 
    public partial class HAP_MainWindow : Window
    {
        //public ObservableCollection<Question> questions;
        public int id_lastSelected = 0;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_RESTORE = 0xF120;


        //public static Question activeQuestion = null;

        //public Question activeQuestion = null;

        ChatListener chat = new ChatListener();

        private ShowQuestionWindow showquestion = new ShowQuestionWindow(); // TODO: Make more resource efficient


        //SummaryExport summary;

        public HAP_MainWindow()
        {
            this.DataContext = this;

            // check if participants in meeting is empty. If it is, let's fill with dummy data
            if (ParticipantManager.instance.participants.Count() == 0)
            {
                Console.WriteLine("this was called at to iniliaze participants");
                ParticipantManager.instance.GetParticipants();
            }


            //questions = new ObservableCollection<Question>();
            InitializeComponent();
            //embedZoom();
            questions_list.ItemsSource = QuestionManager.instance.questions;
            participant_list.ItemsSource = ParticipantManager.instance.participants;
            groups_list.ItemsSource = GroupManager.instance.groups;
            //summary = new SummaryExport();

        }

        public void embedZoom()
        {
            IMeetingUIControllerDotNetWrap uictrl_service = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController();
            ValueType va_1 = new HWNDDotNet();
            ValueType va_2 = new HWNDDotNet();
            uictrl_service.GetMeetingUIWnd(ref va_1, ref va_2);
            // need to check if dual screen or not. The below makes it full screen
            //uictrl_service.EnterFullScreen(true, false);
            HWNDDotNet firstHwd = (HWNDDotNet)va_1;

            SetParent((System.IntPtr)firstHwd.value, CBox.Handle);
            Console.WriteLine("after call GetMeetingUIWnd: firstHwd.value = " + firstHwd.value);
            //SetParent(firstHwd.value,);

            SendMessage((System.IntPtr)firstHwd.value, WM_SYSCOMMAND, SC_MAXIMIZE, 0);

            //Use this opportunity to set up Summary Export
            //BOB is going to move this to somewhere a bit better. This function is called everytime you hope between Zoom Rooms as well
            // you can find it in start_join_meeting.xaml.cs look for on in meeting status. You should see it there

        }
        private void popZoomwindow()
        {
            // Reference: https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setparent
            // https://stackoverflow.com/questions/21635473/remove-parent-of-window-or-form
            IMeetingUIControllerDotNetWrap uictrl_service = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController();
            ValueType va_1 = new HWNDDotNet();
            ValueType va_2 = new HWNDDotNet();
            uictrl_service.GetMeetingUIWnd(ref va_1, ref va_2);
            HWNDDotNet firstHwd = (HWNDDotNet)va_1;
            uictrl_service.ExitFullScreen(true, false);
            SetParent((IntPtr)firstHwd.value, IntPtr.Zero);

            //uint style = GetWindowLong((IntPtr)firstHwd.value, GWL_STYLE);
            //style = (style | WS_POPUP) & (~WS_CHILD);
            //SetWindowLong((IntPtr)firstHwd.value, GWL_STYLE, style);

            SendMessage((System.IntPtr)firstHwd.value, WM_SYSCOMMAND, SC_RESTORE, 0);


        }
        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text.Contains("Select participant") || txtBox.Text.Contains(GlobalVar.default_note))
            {
                txtBox.Text = string.Empty;
            }

        }

        private void participant_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (participant_list.SelectedIndex > 0)
            {
                var student = (Participant)e.AddedItems[0];

                id_lastSelected = ParticipantManager.instance.participants.IndexOf(student);
                if (student.Notes.Equals(GlobalVar.default_note))
                {
                    student.Notes += student.Name;
                }
                NoteTextBox.Text = student.Notes;
            }
            else
            {
                NoteTextBox.Text = "Select participant";
            }

        }

        private void NoteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //change the participants here to  update notes for each student
            if ((ParticipantManager.instance.participants.Count != 0) && (id_lastSelected < ParticipantManager.instance.participants.Count))
            {
                ParticipantManager.instance.participants[id_lastSelected].Notes = NoteTextBox.Text;



            }

        }

        private void Add_Question_Click(object sender, RoutedEventArgs e)
        {
            var addQuestionWindow = new AddQuestionWindow();
            //this was ShowDialog before. Which froze all the other windows
            addQuestionWindow.Show();

        }



        private void usequestion_Click(object sender, RoutedEventArgs e)
        {
            Question problem = (sender as Button).DataContext as Question;

            QuestionManager.instance.activeQuestion = problem;


            //Source: https://stackoverflow.com/questions/381973/how-do-you-tell-if-a-wpf-window-is-closed
            if (PresentationSource.FromVisual(showquestion) == null)
            {
                showquestion = new ShowQuestionWindow();
            }

            showquestion.UpdateQuestion(problem);
            showquestion.Show();
            Console.WriteLine(problem.question);
            problem.used = true;

            WindowInteropHelper helper = new WindowInteropHelper(showquestion); // TODO: Make more resource efficient
            helper.Owner = helper.Handle;

            ZOOM_SDK_DOTNET_WRAP.HWNDDotNet wind = new ZOOM_SDK_DOTNET_WRAP.HWNDDotNet(); // TODO: Make more resource efficient
            wind.value = (uint)helper.Handle;
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingShareController().StartAppShare(wind);

        }
        private void viewresults_Click(object sender, RoutedEventArgs e)
        {
            Question problem = (sender as Button).DataContext as Question;

            var viewresults = new QuestionResultsWindow();
            viewresults.UpdateQuestion(problem);

            viewresults.Show();
        }


        private void SetupUpGroups_click(object sender, RoutedEventArgs e)
        {

            var bo_Settings_Window = new BO_Settings_Window();
            //bo_Settings_Window.ShowDialog();

            bo_Settings_Window.Show();
        }

        void Wnd_Closing(object sender, CancelEventArgs e)
        {
            //LeaveMeetingCmd ID = LeaveMeetingCmd.END_MEETING;
            //CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Leave(ID);
            SummaryExport.instance.WriteSummary();
            System.Windows.Application.Current.Shutdown();
        }

        private void join_Bo_click(object sender, RoutedEventArgs e)
        {
            Group group = (sender as Button).DataContext as Group;

            ZOOM_SDK_DOTNET_WRAP.IMeetingBreakoutRoomsControllerDotNetWrap BO_controller = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingBreakoutRoomsController();
            if (group.group_ID != "")
            {
                //We need to leave an y breakout rooms that we might be in
                if (GroupManager.instance.last_groupID != "")
                {
                    BO_controller.LeaveBreakoutRoom();
                    //TODO: wait for in meeting status then we can call Join Breakout room
                }
                GroupManager.instance.joinedBO_Room = true;
                BO_controller.JoinBreakoutRoom(group.group_ID);
                GroupManager.instance.last_groupID = group.group_ID;
                (sender as Button).Content = "Joined";
            }



            // we are doing this via the group
            Console.WriteLine(group.Participants_in_group.Count);
            // To make sure we have nothing selected whwn we change the order
            participant_list.SelectedIndex = -1;
            for (int i = group.Participants_in_group.Count; i-- > 0;)
            {
                Console.WriteLine("We are here");
                Participant person = group.Participants_in_group[i];

                // above is working
                //It might be that notes are not updated within the observation list

                Participant person_in_list = ParticipantManager.instance.participants.FirstOrDefault(j => j.ID == person.ID);

                int index = ParticipantManager.instance.participants.IndexOf(person_in_list);
                //if index not found we return -1
                if (index > 0)
                {
                    Console.WriteLine("Index for the particapnt we want to remove {0}", index);
                    ParticipantManager.instance.participants.Move(index, 0);
                }

            }

        }



        private void ZoomEmbeddToggle(object sender, RoutedEventArgs e)
        {
            if (Session.instance.zoomEmbedded)
            {
                (sender as Button).Content = "Dock Zoom Window";
                popZoomwindow();
                Session.instance.zoomEmbedded = false;
            }

            else
            {
                (sender as Button).Content = "Pop Zoom Window";
                embedZoom();
                //popZoomwindow();
                Session.instance.zoomEmbedded = true;
            }
        }
    }


}

