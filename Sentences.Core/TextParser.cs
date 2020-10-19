using System.Collections.Generic;
using System.Linq;

namespace Sentences.Core
{
    public interface ITextParser
    {
        IEnumerable<Sentence> Parse(string text);
    }
    
    public class TextParser : ITextParser
    {
        public IEnumerable<Sentence> Parse(string text)
        {
            if (string.IsNullOrEmpty(text)) 
                return new List<Sentence>();
            
            var trimmedText = text.Trim();
            var rawSentences = SplitToSentences(trimmedText);
            return rawSentences.Select(MakeSentence);
        }

        private IEnumerable<string> SplitToSentences(string text)
        {
            var sentences = text.Split('.');
            return sentences
                .Select(s => s.Trim(' ', '\r', '\n'))
                .Where(s => s != string.Empty);
        }
        
        private Sentence MakeSentence(string sentence)
        {
            var rawWords = sentence.Split(' ');
            var words = rawWords
                .Select(w => w.Trim(' ', ',', '\r', '\n'))
                .OrderBy(w => w)
                .Where(s => s != string.Empty);;
            return new Sentence(words);
        }
    }
}