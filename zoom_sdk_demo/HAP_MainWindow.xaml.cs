﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using zoom_sdk_demo.Models;
using ZOOM_SDK_DOTNET_WRAP;
using System.ComponentModel;

namespace zoom_sdk_demo
{
    /// <summary>
    /// Interaction logic for HAP_MainWindow.xaml
    /// </summary>
    /// 
    public partial class HAP_MainWindow : Window
    {
        public ObservableCollection<Question> questions;
        public int id_lastSelected = 0;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_MAXIMIZE = 0xF030;

        public static Question activeQuestion = null;

        ChatListener chat = new ChatListener();

        public HAP_MainWindow()
        {
            this.DataContext = this;

            // check if participants in meeting is empty. If it is, let's fill with dummy data
            if (ParticipantManager.instance.participants.Count() == 0)
            {
                Console.WriteLine("this was called at to iniliaze participants");
                ParticipantManager.instance.GetParticipants();
            }


            questions = new ObservableCollection<Question>();
            InitializeComponent();
            //embedZoom();
            questions_list.ItemsSource = questions;
            participant_list.ItemsSource = ParticipantManager.instance.participants;
            groups_list.ItemsSource = GroupManager.instance.groups;

        }

        public void embedZoom()
        {
            IMeetingUIControllerDotNetWrap uictrl_service = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController();
            ValueType va_1 = new HWNDDotNet();
            ValueType va_2 = new HWNDDotNet();
            uictrl_service.GetMeetingUIWnd(ref va_1, ref va_2);
            // need to check if dual screen or not
            uictrl_service.EnterFullScreen(true, false);
            HWNDDotNet firstHwd = (HWNDDotNet)va_1;

            SetParent((System.IntPtr)firstHwd.value, CBox.Handle);
            Console.WriteLine("after call GetMeetingUIWnd: firstHwd.value = " + firstHwd.value);
            //SetParent(firstHwd.value,);

            SendMessage((System.IntPtr)firstHwd.value, WM_SYSCOMMAND, SC_MAXIMIZE, 0);

        }
        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text.Contains("Write your thoughts here"))
                txtBox.Text = string.Empty;
        }

        private void participant_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var student = (Participant)e.AddedItems[0];

            id_lastSelected = ParticipantManager.instance.participants.IndexOf(student);
            NoteTextBox.Text = student.Notes;
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
            addQuestionWindow.ShowDialog();

        }



        private void usequestion_Click(object sender, RoutedEventArgs e)
        {
            Question problem = (sender as Button).DataContext as Question;

            activeQuestion = problem;

            var showquestion = new ShowQuestionWindow(); // TODO: Make more resource efficient
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


        private void SetupUpGroups_click(object sender, RoutedEventArgs e)
        {

            var bo_Settings_Window = new BO_Settings_Window();
            bo_Settings_Window.ShowDialog();
        }

        void Wnd_Closing(object sender, CancelEventArgs e)
        {
            //LeaveMeetingCmd ID = LeaveMeetingCmd.END_MEETING;
            //CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().Leave(ID);
            System.Windows.Application.Current.Shutdown();
        }

        private void join_Bo_click(object sender, RoutedEventArgs e)
        {
            Group group = (sender as Button).DataContext as Group;

            ZOOM_SDK_DOTNET_WRAP.IMeetingBreakoutRoomsControllerDotNetWrap BO_controller = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingBreakoutRoomsController();
            BO_controller.JoinBreakoutRoom(group.group_ID);

//TODO: get list of people in the BO Group and put their name first

        }
    }
}
