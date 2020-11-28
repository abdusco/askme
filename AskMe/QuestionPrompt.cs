using System.Collections.Generic;

namespace AskMe
{
    public class QuestionPrompt
    {
        private string _key;

        public string Key
        {
            get => _key ?? Question;
            set => _key = value;
        }

        public string Question { get; set; }
        public string Answer { get; set; }

        public QuestionPrompt(string question, string answer = null, string key = null)
        {
            _key = key;
            Question = question;
            Answer = answer;
        }

        private sealed class KeyEqualityComparer : IEqualityComparer<QuestionPrompt>
        {
            public bool Equals(QuestionPrompt x, QuestionPrompt y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x._key == y._key;
            }

            public int GetHashCode(QuestionPrompt obj)
            {
                return (obj._key != null ? obj._key.GetHashCode() : 0);
            }
        }

        public static IEqualityComparer<QuestionPrompt> KeyComparer { get; } = new KeyEqualityComparer();
    }
}