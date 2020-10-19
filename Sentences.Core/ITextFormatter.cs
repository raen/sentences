using System.Collections.Generic;

namespace Sentences.Core
{
    public interface ITextFormatter
    {
        string Format(IEnumerable<Sentence> sentences);
    }
}