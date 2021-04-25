using System.Windows;
using zoom_sdk_demo.Models;

namespace zoom_sdk_demo
{
    /// <summary>
    /// Interaction logic for ShowQuestionWindow.xaml
    /// </summary>
    public partial class ShowQuestionWindow : Window
    {
        public Question problem;

        public ShowQuestionWindow()
        {
            problem = new Question();
            problem.question = "Default text";
            InitializeComponent();

            string name = ParticipantManager.instance.getHelpAPeerAppName();

            if (name != "")
            {
                ChatText.Visibility = Visibility.Visible;
                ChatText.Text = ChatText.Text + " " + name;
            }

            this.DataContext = problem;
        }

        public void UpdateQuestion(Question problem)
        {
            this.problem = problem;
            this.DataContext = this.problem;

        }

     
    }
}
