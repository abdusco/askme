using System.Collections.Generic;

namespace AskMe
{
    public class QuestionPrompt
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        private sealed class QuestionEqualityComparer : IEqualityComparer<QuestionPrompt>
        {
            public bool Equals(QuestionPrompt x, QuestionPrompt y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Question == y.Question;
            }

            public int GetHashCode(QuestionPrompt obj)
            {
                return (obj.Question != null ? obj.Question.GetHashCode() : 0);
            }
        }

        public static IEqualityComparer<QuestionPrompt> QuestionComparer { get; } = new QuestionEqualityComparer();
    }
}