using System;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using AskMe.Core;
using AskMe.Win32;

namespace AskMe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Win32Console.AttachConsole();

            if (!args.Any())
            {
                Console.WriteLine(
                    @"Usage: askme.exe ""question"" [""question=answer"", ""key:question"", ""key:question=answer""...]");
                Exit(1);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var parser = new QuestionParser();
            var errors = parser.TryParse(args, out var qs);
            if (errors.Any())
            {
                Console.Error.WriteLine("Failed to parse questions");
                errors.ForEach(e => Console.Error.WriteLine($"Failed to parse: {e}"));
                Exit(1);
                return;
            }

            using var form = new PromptForm(qs);
            var result = form.ShowDialog();
            if (result != DialogResult.OK)
            {
                Exit(-1);
                return;
            }

            var answers = form.Response.Answers;
            Console.WriteLine(JsonSerializer.Serialize(answers));

            Exit();
        }

        private static void Exit(int code = 0)
        {
            Application.Exit();
            Environment.Exit(code);
        }
    }
}