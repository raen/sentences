using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sentences.Core
{
    public class CsvTextFormatter : ITextFormatter
    {
        private const string WordText = "Word";
        private const string SentenceText = "Sentence";
        
        public string Format(IEnumerable<Sentence> sentences)
        {
            var sentencesList = sentences.ToList();
            if(sentencesList.Count == 0)
                return  string.Empty;
            
            var csv = new StringBuilder();
            csv.AppendLine(PrepareHeaderLine(sentencesList));
            var sentenceLineNo = 1;
            foreach (var sentence in sentencesList)
            {
                var csvLine = PrepareSentenceLine(sentence, sentenceLineNo);
                if (sentencesList.Count == sentenceLineNo)
                    csv.Append(csvLine);
                else
                    csv.AppendLine(csvLine);
                sentenceLineNo++;
            }
            
            return csv.ToString();
        }

        private string PrepareHeaderLine(IEnumerable<Sentence> sentencesList)
        {
            var maxWordCount = sentencesList.Max(s => s.Words.Count());
            var header = string.Empty;
            for (var i = 1; i <= maxWordCount; i++)
                header += $"{WordText} {i}, ";
            return TrimEnd(header);
        }
        
        private string PrepareSentenceLine(Sentence sentence, int lineNo)
        {
            return TrimEnd(sentence.Words.Aggregate(
                $"{SentenceText} {lineNo}, ", 
                (current, word) => current + $"{word}, "
                )
            );
        }

        private string TrimEnd(string text)
        {
            return text.TrimEnd(' ', ',');
        }
    }
}