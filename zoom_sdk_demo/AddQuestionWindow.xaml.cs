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

            ((HAP_MainWindow)Application.Current.MainWindow).questions.Add(item);

            Console.WriteLine(item.answers.ToString());
            Close();
        }
    }
}
