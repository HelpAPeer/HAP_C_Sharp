using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using zoom_sdk_demo.Models;
using ZOOM_SDK_DOTNET_WRAP;

namespace zoom_sdk_demo
{
    /// <summary>
    /// Interaction logic for BO_Settings_Window.xaml
    /// </summary>
    public partial class BO_Settings_Window : Window
    {

        public BO_Settings_Window()
        {
            InitializeComponent();

            this.DataContext = GroupManager.instance.groups;
            groupSize_block.Text = GroupManager.instance.groupSize.ToString();
        }



        public void prepTheGroups()
        {
            if (GroupManager.instance.groups.Count < 1)
            {
                ParticipantManager.instance.EvaluateStudents(QuestionManager.instance.questions);
                GroupManager.instance.getGroups();
            }
        }

        private void IncreaseGroupSize_Click(object sender, RoutedEventArgs e)
        {
            if (GroupManager.instance.groupSize < 10)
            {
                GroupManager.instance.groupSize++;

            }

            groupSize_block.Text = GroupManager.instance.groupSize.ToString();

        }

        private void DecreaseGroupSize_click(object sender, RoutedEventArgs e)
        {
            if (GroupManager.instance.groupSize > 1)
            {
                GroupManager.instance.groupSize--;
            }
            groupSize_block.Text = GroupManager.instance.groupSize.ToString(); ;
        }


        // Need to be host for this to work 
        private void StartBO_Click(object sender, RoutedEventArgs e)
        {
            var groups = GroupManager.instance.groups;
            //intialize the breakout rooms based on the groups made
            // Reference: https://devforum.zoom.us/t/how-to-use-the-ibocreator-class-in-c/26548/2

            //ZOOM_SDK_DOTNET_WRAP.IMeetingBreakoutRoomsControllerDotNetWrap BO_controller = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingBreakoutRoomsController();


            //This only works if you are host, TOOD: need to check if I have host privileges before calling this. It's causing breaks

            Participant me = ParticipantManager.instance.participants.FirstOrDefault(p => p.isMyself == true);

            //If we are not host, we can't create BO rooms. Also if me does not exist that we can't do anything
            if (!(me is null) && me.hasHostPrivileges())
            {

                IMeetingBOControllerDotNetWrap BO_controller = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingBOController();
                Console.WriteLine("can we start BO Rooms {0}", BO_controller.GetBOAdminHelper().CanStartBO());



                //AssignNewUserToRunningBO 

                string id = "";
                foreach (Group g in groups)
                {

                    id = BO_controller.GetBOCreatorHelper().CreateBO(g.Name);

                    int index = groups.IndexOf(g);
                    if (index >= 0)
                    {
                        GroupManager.instance.groups[index].group_ID = id;
                    }

                    //Now we need to assign each member to group
                    //https://devforum.zoom.us/t/startbo-does-not-start-breakout-room/47459
                    //https://devforum.zoom.us/t/breakout-troubles-and-crashes/47452
                    IBODataDotNet BO_data = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingBOController().GetBODataHelper();

                    //TODO;unassasigned users not refereshing
                    //This does not update in real time. If we unassign users, the list doesn't update. This is such trash
                    string[] users_left = BO_data.GetUnassginedUserList();


                    foreach (string user in users_left)
                    {
                        Console.WriteLine("The ID of the user to add {0}. The id of the BO room is {1}", user, id);
                        string name = BO_data.GetBOUserName(user);
                        Console.WriteLine("The username is {0}", name);
                        //we might need to add a wait time between calls here
                        //Thread.Sleep(TimeSpan.FromSeconds(5));

                        //TODO: need to find a better way to add users to grou
                        //Both calls are needed!!

                        if (!(g.Participants_in_group.FirstOrDefault(p => p.Name == name) is null))
                        {

                            bool status = BO_controller.GetBOCreatorHelper().AssignUserToBO(user, id);



                            if (!BO_controller.IsBOStarted())
                            {
                                //Thread.Sleep(TimeSpan.FromSeconds(2));
                                Console.WriteLine("We did not start BO rooms");
                                BO_controller.GetBOAdminHelper().StartBO();
                            }
                            BO_controller.GetBOAdminHelper().AssignNewUserToRunningBO(user, id);

                        }


                    }
                }
                //foreach (Participant p in g.Participants_in_group)
                //{
                //    bool success = BO_controller.GetBOCreatorHelper().AssignUserToBO(p.ID.ToString(), id);
                //    Console.WriteLine("Adding {0} succes:? {1}, ID {2}", p.Name, success, p.ID);
                //}


            }

            //we might need to start Open the Breakout Room Dialog box to start the meeting. 
            //IMeetingUIControllerDotNetWrap uictrl_service = CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetUIController();
            //uictrl_service.ShowSharingToolbar
            Close();
        }


        //if (list_of_BOs.Length > 0)
        //if (!(list_of_BOs is null))
        //{
        //    for (int i = list_of_BOs.GetLowerBound(0); i <= list_of_BOs.GetUpperBound(0); i++)
        //    {
        //        IBreakoutRoomsInfoDotNet breakout_room = (IBreakoutRoomsInfoDotNet)list_of_BOs.GetValue(i);
        //        Console.WriteLine(breakout_room.GetBID());
        //        String b_id = breakout_room.GetBID();
        //        Console.WriteLine(breakout_room.GetBreakoutRoomName());
        //        String b_roomName = breakout_room.GetBreakoutRoomName();
        //        Group group_toModify = groups.FirstOrDefault(group => group.Name.Contains(b_roomName));


        //        if (!(group_toModify is null))
        //        {
        //            Console.WriteLine("We Found something");
        //            group_toModify.group_ID = breakout_room.GetBID();
        //            int index = groups.IndexOf(group_toModify);
        //            groups.RemoveAt(index);
        //            groups.Insert(index, group_toModify);
        //        }

        //        //BO_controller.JoinBreakoutRoom(breakout_room.GetBID());
        //    }
        //}





        private void update_click(object sender, RoutedEventArgs e)
        {
            ParticipantManager.instance.EvaluateStudents(QuestionManager.instance.questions);
            GroupManager.instance.getGroups();
        }

        private void cancel_click(object sender, RoutedEventArgs e)
        {
            GroupManager.instance.groups.Clear();
            Close();
        }

        private void ShareGroup_Click(object sender, RoutedEventArgs e)
        {
            const string stop_share = "Stop Sharing Groups";

            ZOOM_SDK_DOTNET_WRAP.HWNDDotNet wind = new ZOOM_SDK_DOTNET_WRAP.HWNDDotNet(); // TODO: Make more resource efficient
            IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            wind.value = (uint)windowHandle;

            if ((sender as Button).Content.Equals(stop_share))
            {
                (sender as Button).Content = "Share Groups";
                HeaderToHide.Visibility = Visibility.Visible;
                ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingShareController().StopShare();
                return;
            }


            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingShareController().StartAppShare(wind);

            //TODO: Need to hide the top part
            HeaderToHide.Visibility = Visibility.Collapsed;
            (sender as Button).Content = stop_share;

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            GroupManager.instance.groupType = 0;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            GroupManager.instance.groupType = 1;
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            GroupManager.instance.groupType = 2;
        }

        private void save_click(object sender, RoutedEventArgs e)
        {
            SummaryExport.instance.SaveGroups(GroupManager.instance.groups);
        }
    }
}
