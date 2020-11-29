using System.Collections.Generic;
using System.Linq;
using AskMe.Core;

namespace AskMe.Wpf
{
    public class QueryFormViewModel
    {
        public List<QuestionViewModel> Questions { get; set; }

        public QueryFormViewModel(IEnumerable<PromptQuery> queries)
        {
            Questions = queries.Select(q => new QuestionViewModel(q)).ToList();
        }

        public PromptResponse ToPromptResponse()
        {
            var res = new PromptResponse();
            Questions.ForEach(q => res.AddAnswer(q.Key, q.Answer ?? ""));
            return res;
        }

        public class QuestionViewModel
        {
            public string Question { get; set; }
            public string Answer { get; set; }
            public string Key { get; set; }

            public QuestionViewModel(PromptQuery q)
            {
                Question = q.Question;
                Answer = q.Answer;
                Key = q.Key;
            }
        }
    }
}