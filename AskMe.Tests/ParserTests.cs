using System;
using Xunit;

namespace AskMe.Tests
{
    public class ParserTests
    {
        [Fact]
        public void CanParseSimpleQuestions()
        {
            const string q = "question";
            var parser = new QuestionParser();

            var result = parser.TryParse(q, out var prompt);

            Assert.True(result);
            Assert.Equal(q, prompt.Question);
            Assert.Equal(q, prompt.Key);
        }

        [Fact]
        public void CanParseQuestionAndAnswer()
        {
            const string q = "question=answer";
            var parser = new QuestionParser();

            var result = parser.TryParse(q, out var prompt);

            Assert.True(result);
            Assert.Equal("question", prompt.Question);
            Assert.Equal("question", prompt.Key);
            Assert.Equal("answer", prompt.Answer);
        }

        [Fact]
        public void CanParseKeyQuestion()
        {
            const string q = "key:question";
            var parser = new QuestionParser();

            var result = parser.TryParse(q, out var prompt);

            Assert.True(result);
            Assert.Equal("question", prompt.Question);
            Assert.Equal("key", prompt.Key);
            Assert.Null(prompt.Answer);
        }

        [Fact]
        public void CanParseKeyQuestionAndAnswer()
        {
            const string q = "key:question=answer";
            var parser = new QuestionParser();

            var result = parser.TryParse(q, out var prompt);

            Assert.True(result);
            Assert.Equal("question", prompt.Question);
            Assert.Equal("key", prompt.Key);
            Assert.Equal("answer", prompt.Answer);
        }
         
        [Fact]
        public void IgnoresEmptyAnswer()
        {
            const string q = "question=";
            var parser = new QuestionParser();

            var result = parser.TryParse(q, out var prompt);

            Assert.True(result);
            Assert.Equal("question", prompt.Question);
            Assert.Null(prompt.Answer);
        }
        
        [Fact]
        public void FailsWithEmptyQuestion()
        {
            const string q = "key:";
            var parser = new QuestionParser();

            var result = parser.TryParse(q, out var prompt);

            Assert.False(result);
            Assert.Null(prompt);
        }
        

        [Fact]
        public void CanAddAnswers()
        {
            var result = new PromptResult();
            const string key = "q";
            var p = new QuestionPrompt("hey", key: key);
            
            result.AddAnswer(p, "hi");

            Assert.Equal("hi", result.Answers[key]);
        }
    }
}