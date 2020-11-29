using System;
using System.Collections.Generic;

namespace AskMe.Core
{
    public class QuestionParser
    {
        public List<string> TryParse(string[] arguments, out List<PromptQuery> prompts)
        {
            var errors = new List<string>();
            prompts = new List<PromptQuery>();
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

        public bool TryParse(string argument, out PromptQuery prompt)
        {
            prompt = null;
            string key = null;
            string question = null;
            string answer = null;
            
            var remaining = argument;
            if (remaining.Contains(":"))
            {
                var parts = remaining.Split(new[] {":"}, 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2)
                {
                    return false;
                }
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

            prompt = new PromptQuery(question, answer, key);
            return true;
        }


        private bool TryParseQA(string argument, out string question, out string answer)
        {
            var parts = argument.Split(new[] {"="}, 2, StringSplitOptions.RemoveEmptyEntries);
            question = null;
            answer = null;
            if (parts.Length > 1)
            {
                question = parts[0];
                answer = parts[1];
                return true;
            }

            if (parts.Length == 1)
            {
                question = parts[0];
                return true;
            }

            return false;
        }
    }
}