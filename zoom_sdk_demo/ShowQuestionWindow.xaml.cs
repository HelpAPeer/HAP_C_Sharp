﻿using System;
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
            this.DataContext = problem;
        }

        public void UpdateQuestion(Question problem)
        {
            this.problem = problem;
            this.DataContext = this.problem;
        }
    }
}