using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AskMe.Core;

namespace AskMe.Wpf
{
    public partial class QueryFormWindow
    {
        public QueryFormViewModel Model { get; }

        public QueryFormWindow(QueryFormViewModel model)
        {
            Model = model;
            DataContext = model;
            
            InitializeComponent();
            QuestionsIc.ItemsSource = model.Questions;
        }

        private void MainWindow_OnContentRendered(object sender, EventArgs e)
        {
            // prevent resizing below the minimum to prevent cropping
            MaxHeight = ActualHeight;
            MinWidth = ActualWidth;
            MinHeight = ActualHeight;
        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CloseCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void QueryFormWindow_OnDeactivated(object sender, EventArgs e)
        {
            Topmost = false;
        }
    }
}