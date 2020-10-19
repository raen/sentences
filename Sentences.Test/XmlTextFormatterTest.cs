using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;
using Sentences.Core;

namespace Sentences.Test
{
    public class XmlTextFormatterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Format_EmptyText_ReturnsEmptyXml()
        {
            var xml = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),     
                new XElement("text")
            );
            var expectedXml = new StringBuilder()
                .AppendLine(xml.Declaration.ToString())
                .Append(xml)
                .ToString();
            
            var formatter = new XmlTextFormatter();
            var text = formatter.Format(new List<Sentence>());

            Assert.AreEqual(expectedXml, text);
        }
        
        [Test]
        public void Format_FewValidSentences_ReturnsListOfSentences()
        {
            var formatter = new XmlTextFormatter();
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
            expectedXml.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
            expectedXml.AppendLine("<text>");
            expectedXml.AppendLine("    <sentence>");
            expectedXml.AppendLine("        <word>a</word>");
            expectedXml.AppendLine("        <word>had</word>");
            expectedXml.AppendLine("        <word>lamb</word>");
            expectedXml.AppendLine("        <word>little</word>");
            expectedXml.AppendLine("        <word>Mary</word>");
            expectedXml.AppendLine("    </sentence>");
            expectedXml.AppendLine("    <sentence>");
            expectedXml.AppendLine("        <word>Aesop</word>");
            expectedXml.AppendLine("        <word>and</word>");
            expectedXml.AppendLine("        <word>called</word>");
            expectedXml.AppendLine("        <word>came</word>");
            expectedXml.AppendLine("        <word>for</word>");
            expectedXml.AppendLine("        <word>Peter</word>");
            expectedXml.AppendLine("        <word>the</word>");
            expectedXml.AppendLine("        <word>wolf</word>");
            expectedXml.AppendLine("    </sentence>");
            expectedXml.AppendLine("    <sentence>");
            expectedXml.AppendLine("        <word>Cinderella</word>");
            expectedXml.AppendLine("        <word>likes</word>");
            expectedXml.AppendLine("        <word>shoes</word>");
            expectedXml.AppendLine("    </sentence>");
            expectedXml.Append("</text>");
            
            Assert.AreEqual(expectedXml.ToString(), text);
        }
    }
}