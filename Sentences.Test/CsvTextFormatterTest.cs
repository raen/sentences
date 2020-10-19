using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Sentences.Core;

namespace Sentences.Test
{
    public class CsvTextFormatterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Format_EmptyText_ReturnsEmptyXml()
        {
            var formatter = new CsvTextFormatter();
            var text = formatter.Format(new List<Sentence>());

            Assert.AreEqual(string.Empty, text);
        }
        
        [Test]
        public void Format_FewValidSentences_ReturnsListOfSentences()
        {
            var formatter = new CsvTextFormatter();
            var sentences = new List<Sentence>
            {
                new Sentence(new List<string>
                {
                    "a",
                    "had",
                    "lamb",
                    "little",
                    "Mary"
                }),
                new Sentence(new List<string>
                {
                    "Aesop",
                    "and",
                    "called",
                    "came",
                    "for",
                    "Peter",
                    "the",
                    "wolf"
                }),
                new Sentence(new List<string> {"Cinderella", "likes", "shoes"})
            };
            var text = formatter.Format(sentences);

            var expectedXml = new StringBuilder();
            expectedXml.AppendLine("Word 1, Word 2, Word 3, Word 4, Word 5, Word 6, Word 7, Word 8");
            expectedXml.AppendLine("Sentence 1, a, had, lamb, little, Mary");
            expectedXml.AppendLine("Sentence 2, Aesop, and, called, came, for, Peter, the, wolf");
            expectedXml.Append("Sentence 3, Cinderella, likes, shoes");
            
            Assert.AreEqual(expectedXml.ToString(), text);
        }
    }
}