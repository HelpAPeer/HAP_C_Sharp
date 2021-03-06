using System;
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
using zoom_sdk_demo.Models;

namespace zoom_sdk_demo
{
    /// <summary>
    /// Interaction logic for HAP_MainWindow.xaml
    /// </summary>
    /// 
    public partial class HAP_MainWindow : Window
    {
        public ObservableCollection<Participant> participants;

        public HAP_MainWindow()
        {
            InitializeComponent();
            participants = ParticipantManager.GetParticipants();
            DataContext = participants;

        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text == "Place your thoughts here")
                txtBox.Text = string.Empty;
        }

        private void participant_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var student=(Participant)e.AddedItems[0];
            NoteTextBox.Text=student.Notes;
        }
    }
}
