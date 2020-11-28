using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AskMe
{
    public class PromptResult
    {
        private Dictionary<string, string> _answers = new Dictionary<string, string>();

        public ReadOnlyDictionary<string, string> Answers => new ReadOnlyDictionary<string, string>(_answers);

        public void AddAnswer(string key, string value) => _answers[key] = value;
        public void AddAnswer(QuestionPrompt prompt, string answer) => _answers[prompt.Key] = answer;
    }
}