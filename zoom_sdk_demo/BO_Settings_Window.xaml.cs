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
        int groupsize = 3;
        public BO_Settings_Window()
        {
            InitializeComponent();
            GroupManager.instance.getGroups();
            this.DataContext = GroupManager.instance.groups;
        }




        private void IncreaseGroupSize_Click(object sender, RoutedEventArgs e)
        {
            if (groupsize < 10)
            {
                groupsize++;
            }

            groupSize_block.Text = groupsize.ToString();

        }

        private void DecreaseGroupSize_click(object sender, RoutedEventArgs e)
        {
            if (groupsize > 0)
            {
                groupsize--;
            }
            groupSize_block.Text = groupsize.ToString(); ;
        }


    }
}
