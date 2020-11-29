using System;
using System.Linq;
using System.Text.Json;
using System.Windows;
using AskMe.Core;

namespace AskMe.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var parser = new QuestionParser();
            var errors = parser.TryParse(e.Args, out var questions);
            if (errors.Any())
            {
                Console.Error.WriteLine("Failed to parse arguments");
                errors.ForEach(err => Console.Error.WriteLine(err));
            }

            if (!questions.Any())
            {
                Console.WriteLine(
                    @"Usage: askme.exe ""question"" [""question=answer"", ""key:question"", ""key:question=answer""...]");
                ExitWithCode(1);
                return;
            }

            var dialog = new QueryFormWindow(new QueryFormViewModel(questions));
            var submitted = dialog.ShowDialog() ?? false;
            if (!submitted)
            {
                ExitWithCode(-1);
                return;
            }

            var response = dialog.Model.ToPromptResponse();
            Console.WriteLine(JsonSerializer.Serialize(response.Answers));
        }

        private void ExitWithCode(int code)
        {
            Environment.ExitCode = code;
            Application.Current.Shutdown(code);
        }

    }
}