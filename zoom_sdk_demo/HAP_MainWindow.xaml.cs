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
        public ObservableCollection<Question> questions;
        public int id_lastSelected = 0;

        ShowQuestionWindow showQuestionWindow = new ShowQuestionWindow();

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
            questions_list.ItemsSource = questions;
            participant_list.ItemsSource = ParticipantManager.instance.participants;

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
            if ((ParticipantManager.instance.participants.Count != 0) &&( id_lastSelected<ParticipantManager.instance.participants.Count) )
            {
                ParticipantManager.instance.participants[id_lastSelected].Notes = NoteTextBox.Text;

            }

        }

        private void Add_Question_Click(object sender, RoutedEventArgs e)
        {
            var addQuestionWindow = new AddQuestionWindow();
            addQuestionWindow.ShowDialog();

        }

        private void Use_Question_Click(object sender, RoutedEventArgs e)
        {
            showQuestionWindow.question = "test pass!";
        }
    }
}
