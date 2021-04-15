using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (GroupManager.instance.groups.Count < 1)
            {
                ParticipantManager.instance.EvaluateStudents(((HAP_MainWindow)Application.Current.MainWindow).questions);
                GroupManager.instance.getGroups();
            }
            this.DataContext = GroupManager.instance.groups;
            groupSize_block.Text = GroupManager.instance.groupSize.ToString();
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
            ZOOM_SDK_DOTNET_WRAP.IMeetingBreakoutRoomsControllerDotNetWrap BO_controller = ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingBreakoutRoomsController();

            // only gets all the breakout rooms when you are host
            Array list_of_BOs = BO_controller.GetBreakoutRoomsInfoList();

            //if (list_of_BOs.Length > 0)
            if (!(list_of_BOs is null))
            {
                for (int i = list_of_BOs.GetLowerBound(0); i <= list_of_BOs.GetUpperBound(0); i++)
                {
                    IBreakoutRoomsInfoDotNet breakout_room = (IBreakoutRoomsInfoDotNet)list_of_BOs.GetValue(i);
                    Console.WriteLine(breakout_room.GetBID());
                    String b_id = breakout_room.GetBID();
                    Console.WriteLine(breakout_room.GetBreakoutRoomName());
                    String b_roomName = breakout_room.GetBreakoutRoomName();
                    Group group_toModify = groups.FirstOrDefault(group => group.Name.Contains(b_roomName));


                    if (!(group_toModify is null))
                    {
                        Console.WriteLine("We Found something");
                        group_toModify.group_ID = breakout_room.GetBID();
                        int index = groups.IndexOf(group_toModify);
                        groups.RemoveAt(index);
                        groups.Insert(index, group_toModify);
                    }

                    //BO_controller.JoinBreakoutRoom(breakout_room.GetBID());
                }
            }

            //TODO:need to fix
            //foreach (IBreakoutRoomsInfoDotNet bo in list_of_BOs)
            //{
            //    Console.WriteLine(bo.GetBID());
            //    Console.WriteLine(bo.GetBreakoutRoomName());
            //}

            Close();


        }

        private void update_click(object sender, RoutedEventArgs e)
        {
            ParticipantManager.instance.EvaluateStudents(((HAP_MainWindow)Application.Current.MainWindow).questions);
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

            if ((sender as Button).Content.Equals(stop_share)){
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
