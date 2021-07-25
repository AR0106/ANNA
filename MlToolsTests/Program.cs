using System;
using System.Collections;
using System.Collections.Generic;
using AnnaMLTools.General;
using AnnaMLTools.Keyword;
using AnnaMLTools.Regression;
using Word2vec.Tools;

namespace MlToolsTests
{
    class Program
    {
        static void Main()
        {
            /*Tokenizer.Sentence[] timeSet = Tokenizer.Encode("What time is it? What is the current time? What is the time?");
            Vectorizer.InitVectorizer("vectorsMed.bin");
            Console.WriteLine("Vectorizer Initialized...");

            Tokenizer.Sentence[] sentences = Tokenizer.Encode(Console.ReadLine());

            List<double> LFrequencies = new();

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
            }*/

            List<double> xValues = new List<double>();
            xValues.Add(1);
            xValues.Add(2);
            xValues.Add(3);
            xValues.Add(4);
            xValues.Add(5);
            xValues.Add(6);

            List<double> yValues = new List<double>();
            yValues.Add(2);
            yValues.Add(4);
            yValues.Add(8);
            yValues.Add(16);
            yValues.Add(64);
            yValues.Add(4096);

            LinearRegression.TrainLinearModel(xValues, yValues);
            LinearRegression.TrainCorrector(xValues, yValues, 2, 1.4, 0.6);

            Console.WriteLine(LinearRegression.averageError);

            Console.WriteLine(LinearRegression.Fit(2));
            Console.WriteLine(LinearRegression.Predict(2));
        }
    }
}
