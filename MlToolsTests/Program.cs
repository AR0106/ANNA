using System;
using System.Collections;
using System.Collections.Generic;
using AnnaMLTools.General;
using AnnaMLTools.Keyword;
using Word2vec.Tools;

namespace MlToolsTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokenizer.Sentence[] timeSet = Tokenizer.Encode("What time is it? What is the current time? What is the time?");
            Vectorizer.InitVectorizer();
            Console.WriteLine("Vectorizer Initialized...");

            Tokenizer.Sentence[] sentences = Tokenizer.Encode(Console.ReadLine());

            List<double> LFrequencies = new List<double>();

            foreach (var sentence in sentences)
            {
                foreach (var word in sentence.words_noUseless)
                {
                    LFrequencies.Add(tfidf.GetTFIDF(word, sentence, timeSet));

                    DistanceTo[] vectorList = Vectorizer.Vectorize(sentence, 5);

                    foreach (var item in vectorList)
                    {
                        LFrequencies.Add(tfidf.GetTFIDF(item.Representation.WordOrNull, sentence, timeSet));
                    }
                }
            }

            LFrequencies.Sort();
            LFrequencies.Reverse();

            foreach (var frequency in LFrequencies)
            {
                Console.WriteLine(frequency);
            }
        }
    }
}
