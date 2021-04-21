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
    /// Interaction logic for QuestionResultsWindow.xaml
    /// </summary>
    public partial class QuestionResultsWindow : Window
    {
        public Question problem;

        public QuestionResultsWindow()
        {
            problem = new Question();
            problem.question = "Default text";
            InitializeComponent();
            this.DataContext = problem;
        }

        public void UpdateQuestion(Question problem)
        {
            this.problem = problem;
            this.DataContext = this.problem;
            this.problem.PropertyChanged += Question_PropertyChanged;
            response_list.ItemsSource = this.problem.responses;
        }

        public void RefreshList()
        {
            response_list.Items.Refresh();
        }

        private void Question_PropertyChanged(object sender, System.EventArgs e)
        {
            Console.WriteLine("Detected property changed");
            RefreshList();
        }

        private void MarkCorrect_Click(object sender, RoutedEventArgs e)
        {
            KeyValuePair<string, Tuple<string, bool>> student = (KeyValuePair<string, Tuple<string, bool>>)(sender as Button).DataContext;
            problem.MarkResponseCorrect(student.Key);

            response_list.Items.Refresh();
            //problem.responses[student.Key] = new Tuple<string, bool> (student.Value.Item1, true);
            //Question focus = 
        }
    }
}
