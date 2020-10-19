using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sentences.Core;

namespace Sentences.Test
{
    public class TextParserTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Parse_Null_ReturnsEmptyCollection()
        {
            var parser = new TextParser();

            var sentences = parser.Parse(null).ToList();
            
            CollectionAssert.IsEmpty(sentences);
        }
        
        [Test]
        public void Parse_EmptyText_ReturnsEmptyCollection()
        {
            var parser = new TextParser();

            var sentences = parser.Parse(string.Empty).ToList();
            
            CollectionAssert.IsEmpty(sentences);
        }

        [Test]
        public void Parse_FewValidSentences_ReturnsListOfSentences()
        {
            var text ="Mary had a little lamb. Peter called for the wolf, and Aesop came. \nCinderella likes shoes.";
            var parser = new TextParser();

            var sentences = parser.Parse(text).ToList();

            Assert.AreEqual(3, sentences.Count);
            CollectionAssert.AreEqual(
                new List<string>{"a", "had", "lamb", "little", "Mary"}, 
                sentences[0].Words.ToList()
            );
            CollectionAssert.AreEqual(
                new List<string>{"Aesop", "and", "called", "came", "for", "Peter", "the", "wolf"}, 
                sentences[1].Words.ToList()
            );
            CollectionAssert.AreEqual(
                new List<string>{"Cinderella", "likes", "shoes"}, 
                sentences[2].Words.ToList()
            );
        }
        
        [Test]
        public void Parse_FewValidSentencesUnformatted_ReturnsListOfSentences()
        {
            var text = new StringBuilder();
            text.AppendLine("  Mary   had a little  lamb  . ");
            text.AppendLine(" ");
            text.AppendLine();
            text.AppendLine("  Peter   called for the wolf   ,  and Aesop came .");
            text.AppendLine(" Cinderella  likes shoes.");
                
            var parser = new TextParser();

            var sentences = parser.Parse(text.ToString()).ToList();
            
            Assert.AreEqual(3, sentences.Count);
            CollectionAssert.AreEqual(
                new List<string>{"a", "had", "lamb", "little", "Mary"}, 
                sentences[0].Words.ToList()
            );
            CollectionAssert.AreEqual(
                new List<string>{"Aesop", "and", "called", "came", "for", "Peter", "the", "wolf"}, 
                sentences[1].Words.ToList()
            );
            CollectionAssert.AreEqual(
                new List<string>{"Cinderella", "likes", "shoes"}, 
                sentences[2].Words.ToList()
            );
        }
    }
}
