using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnaMLTools.PreTraining
{
    public class tfidf
    {
        public static double GetTF(string term, Tokenizer.Sentence data)
        {
            int frequency = 0;

            foreach (var word in data.words_noUseless)
            {
                if (word == term)
                {
                    frequency++;
                }
            }

            return Math.Log(1 + frequency);
        }

        public static double GetIDF(string term, Tokenizer.Sentence[] dataset)
        {
            int frequency = 0;

            foreach (var sentence in dataset)
            {
                foreach (var word in sentence.words_noUseless)
                {
                    if (word == term)
                    {
                        frequency++;
                    }
                }
            }

            try
            {
                return Math.Log(dataset.Length / frequency);
            }
            catch (DivideByZeroException)
            {
                return frequency;
            }
        }

        public static double GetTFIDF(string term, Tokenizer.Sentence data, Tokenizer.Sentence[] dataset)
        {
            return GetTF(term, data) * GetIDF(term, dataset);
        }
    }
}
