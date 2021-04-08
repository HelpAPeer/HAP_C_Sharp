using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using zoom_sdk_demo.Models;
using System.IO;

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
        }



        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var item = (Question)(DataContext);

            item.answers = new List<string>(item.answerString.Split('\n'));

            ((HAP_MainWindow)System.Windows.Application.Current.MainWindow).questions.Add(item);

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
            questions.ForEach(((HAP_MainWindow)System.Windows.Application.Current.MainWindow).questions.Add);
        }

        private void Answer_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void Add_image_click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Add Image to Quesiton";
            dialog.Filter = "Image Files(*.BMP, *.JPG, *.GIF, *.PNG)| *.BMP; *.JPG; *.GIF; *.PNG; *.png; *.jpeg | All files(*.*) | *.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                image_filepath.Text = dialog.FileName;
            }
            else
            {
                return;
            }
        }
    }
}
