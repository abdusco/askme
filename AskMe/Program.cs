using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

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
            Win32Native.AttachConsole();
            if (!args.Any())
            {
                Console.WriteLine(@"Usage: prompt.exe ""question"" [""question=answer""...]");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            List<QuestionPrompt> qs;
            try
            {
                qs = ParseArgs(args);
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Failed to parse questions");
                Application.Exit();
                Exit(1);
                return;
            }

            using var f = new PromptForm(qs);

            var result = f.ShowDialog();
            if (result != DialogResult.OK)
            {
                Exit(-1);
            }

            var answers = f.Answers;
            Console.WriteLine(JsonSerializer.Serialize(answers));

            Exit(0);
        }

        private static void Exit(int code = 0)
        {
            Application.Exit();
            Environment.Exit(code);
        }

        private static List<QuestionPrompt> ParseArgs(string[] args)
        {
            return args.Select(item =>
            {
                var parts = item.Split(new[] {"="}, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    return new QuestionPrompt
                    {
                        Question = parts[0],
                        Answer = parts[1]
                    };
                }

                return new QuestionPrompt
                {
                    Question = item.Trim('=')
                };
            }).Distinct(QuestionPrompt.QuestionComparer).ToList();
        }
    }
}