using System.Collections.Generic;

namespace Sentences.Core
{
    public class Sentence
    {
        public Sentence(IEnumerable<string> words)
        {
            Words = words;
        }

        public IEnumerable<string> Words { get; }
    }
}