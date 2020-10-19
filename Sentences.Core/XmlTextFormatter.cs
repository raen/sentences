using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Sentences.Core
{
    public class XmlTextFormatter : ITextFormatter
    {
        private const string XmlDeclarationVersion = "1.0";
        private const string XmlDeclarationEncoding = "UTF-8";
        private const string XmlDeclarationStandalone = "yes";

        private const string XmlRootElementText = "text";
        private const string XmlSentenceElementText = "sentence";
        private const string XmlWordElementText = "word";

        private const int XmlDefaultSpaces = 2;
        private const int XmlExpectedSpaces = 4;
        
        public string Format(IEnumerable<Sentence> sentences)
        {
            var xmlDocument = CreateXmlDocument(sentences);
            var xmlText = XmlToString(xmlDocument);
            return Justify(xmlText);
        }

        private string XmlToString(XDocument xmlDocument)
        {
            var xmlText = new StringBuilder();
            xmlText.AppendLine(xmlDocument.Declaration.ToString());
            xmlText.Append(xmlDocument);
            return xmlText.ToString();
        }

        private XDocument CreateXmlDocument(IEnumerable<Sentence> sentences)
        {
            var textElement = new XElement(XmlRootElementText);
            var xml = new XDocument(
                new XDeclaration(XmlDeclarationVersion, XmlDeclarationEncoding, XmlDeclarationStandalone),
                textElement
            );

            foreach (var sentence in sentences)
            {
                var sentenceElement = new XElement(XmlSentenceElementText);
                foreach (var word in sentence.Words)
                {
                    sentenceElement.Add(new XElement(XmlWordElementText, word));
                }

                textElement.Add(sentenceElement);
            }

            return xml;
        }


        private string Justify(string xmlText)
        {
            return xmlText.Replace(
                new string(' ', XmlDefaultSpaces), 
                new string(' ', XmlExpectedSpaces)
                );
        }
    }
}