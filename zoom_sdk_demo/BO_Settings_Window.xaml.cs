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



        private void StartBO_Click(object sender, RoutedEventArgs e)
        {
            var groups = GroupManager.instance.groups;
            //intialize the breakout rooms based on the groups made

            Close();


        }

        private void update_click(object sender, RoutedEventArgs e)
        {
            GroupManager.instance.getGroups();
        }

        private void cancel_click(object sender, RoutedEventArgs e)
        {
            GroupManager.instance.groups.Clear();
            Close();
        }

        private void ShareGroup_Click(object sender, RoutedEventArgs e)
        {
            ZOOM_SDK_DOTNET_WRAP.HWNDDotNet wind = new ZOOM_SDK_DOTNET_WRAP.HWNDDotNet(); // TODO: Make more resource efficient
            IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            wind.value = (uint)windowHandle;
            ZOOM_SDK_DOTNET_WRAP.CZoomSDKeDotNetWrap.Instance.GetMeetingServiceWrap().GetMeetingShareController().StartAppShare(wind);
        }
    }
}
