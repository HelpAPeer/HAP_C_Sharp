using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Forms;
using zoom_sdk_demo.Models;

namespace zoom_sdk_demo
{
    /// <summary>
    /// Interaction logic for AddQuestionWindow.xaml
    /// </summary>
    public partial class AddQuestionWindow : Window
    {
        public AddQuestionWindow()
        {
            DataContext = new Question();
            InitializeComponent();
            questions_list.ItemsSource = QuestionManager.instance.questions;
        }







        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var item = (Question)(DataContext);

            item.answers = new List<string>(item.answerString.Split('\n'));
            QuestionManager.instance.questions.Add(item);

            Console.WriteLine(item.answers.ToString());
            Close();
        }



        private void import_Questions_click(object sender, RoutedEventArgs e)
        {
            //TODO: open file dialog to get CSV
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open Question CSV File";
            dialog.Filter = "CSV Files (*.csv)|*.csv";
            string filename = "";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = dialog.FileName;
            }
            else
            {
                return;
            }
            // Import questions from CSV into file
            //

            // We need to skip the header file in this case

            List<Question> questions = File.ReadAllLines(filename).Skip(1)
                                           .Select(v => Question.FromCsv(v))
                                           .ToList();

            //Remove any question in list that has an empty answer and question
            questions = questions.Where(q => (q.question != "") || (q.answerString != "")).ToList();
            //Add the list of quesitons to the observable collection
            questions.ForEach(QuestionManager.instance.questions.Add);
        }

        private void Answer_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void Add_image_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Add Image to Quesiton";
            dialog.Filter = "Image Files(*.BMP, *.JPG, *.GIF, *.PNG)| *.BMP; *.JPG; *.GIF; *.PNG; *.png; *.JPEG | All files(*.*) | *.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                image_filepath.Text = dialog.FileName;
            }
            else
            {
                return;
            }
        }

        private void questions_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Resource
            //https://stackoverflow.com/questions/34314339/display-selected-listbox-items-data-in-wpf
            System.Windows.Controls.ListView lb = sender as System.Windows.Controls.ListView;
            Question quesiton = (Question)lb.SelectedItem;
            if (!(quesiton is null))
            {
                Console.WriteLine(quesiton.question);
                DataContext = quesiton;
            }

        }



        private void New_Question_Click(object sender, RoutedEventArgs e)
        {
            Question question = new Question();
            QuestionManager.instance.questions.Add(question);
            questions_list.SelectedItem = question;

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Question question = (Question)questions_list.SelectedItem;
            questions_list.SelectedIndex = -1;
            QuestionManager.instance.questions.Remove(question);
            DataContext = new Question();
            //We might need to change the selected item to something else

        }
    }
}
