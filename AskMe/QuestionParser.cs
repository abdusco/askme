using System;
using System.Collections.Generic;

namespace AskMe
{
    public class QuestionParser
    {
        public List<string> TryParse(string[] arguments, out List<QuestionPrompt> prompts)
        {
            var errors = new List<string>();
            prompts = new List<QuestionPrompt>();
            foreach (var argument in arguments)
            {
                if (!TryParse(argument, out var p))
                {
                    errors.Add(argument);
                    continue;
                }

                prompts.Add(p);
            }

            return errors;
        }

        public bool TryParse(string argument, out QuestionPrompt prompt)
        {
            prompt = null;
            var remaining = argument;
            var parts = remaining.Split(new[] {":"}, 2, StringSplitOptions.RemoveEmptyEntries);

            string key = null;
            string question;
            string answer;
            if (parts.Length == 2)
            {
                key = parts[0];
                remaining = parts[1];
            }

            string parsedQuestion;
            string parsedAnswer;
            if (TryParseQA(remaining, out parsedQuestion, out parsedAnswer))
            {
                question = parsedQuestion;
                answer = parsedAnswer;
            }
            else
            {
                question = remaining;
                answer = null;
            }


            prompt = new QuestionPrompt(question, answer, key);
            return true;
        }


        private bool TryParseQA(string argument, out string question, out string answer)
        {
            var parts = argument.Split(new[] {"="}, 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                question = parts[0];
                answer = parts[1];
                return true;
            }

            question = null;
            answer = null;
            return false;
        }
    }
}