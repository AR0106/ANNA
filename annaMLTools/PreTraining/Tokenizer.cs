using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AnnaMLTools.PreTraining
{
    public class Tokenizer
    {
        public enum SentenceType
        {
            Statement,
            Question,
            ExclaimedStatement,
            Unmarked
        }

        public struct Sentence
        {
            public string text;
            public SentenceType sentenceType;
            public string[] words;
            public string[] words_noUseless;
            public string[] bigrams;
            public string[] trigrams;
        }

        private static string[] GenNGrams(int n, Sentence sentence)
        {
            List<string> LNgrams = new List<string>();

            int gramLayers = -1;

            // Populate LNgrams
            for (int i = 0; i < sentence.words_noUseless.Length; i++)
            {
                if (i % n == 0)
                {
                    gramLayers++;
                    LNgrams.Add(sentence.words_noUseless[i]);
                }
                else
                {
                    LNgrams[gramLayers] = LNgrams[gramLayers] + "/" + sentence.words_noUseless[i];
                }
            }

            // Set Sentence Trigrams to the Newly Populated LTrigrams
            return LNgrams.ToArray();
        }

        public static string[] uselessWords = 
        { 
            "the ",
            "an ",
            "a ",
            "who ",
            "what ",
            "when ",
            "where ",
            "why ",
            "how ",
            "that ",
            "is ",
            "works "
        };

        public static Sentence[] Encode(string text)
        {
            string[] splitSentences = Regex.Split(text, @"(?<=[\.!\?])\s+");

            List<Sentence> LSentences = new List<Sentence>();

            foreach (string item in splitSentences)
            {
                Sentence sentence = new Sentence();
                sentence.text = item;
                sentence.words = item.ToLower().Split(' ');

                string noUseless = item.ToLower();

                foreach (var useless in uselessWords)
                {
                    noUseless = noUseless.ToLower().Replace(useless, "");
                }

                sentence.words_noUseless = noUseless.Split(' ');

                if (item.EndsWith('.'))
                {
                    sentence.sentenceType = SentenceType.Statement;
                }
                else if (item.EndsWith('?'))
                {
                    sentence.sentenceType = SentenceType.Question;
                }
                else if (item.EndsWith('!'))
                {
                    sentence.sentenceType = SentenceType.ExclaimedStatement;
                }
                else
                {
                    sentence.sentenceType = SentenceType.Unmarked;
                }

                sentence.trigrams = GenNGrams(3, sentence);

                sentence.bigrams = GenNGrams(2, sentence);

                LSentences.Add(sentence);
            }

            return LSentences.ToArray();
        }
    }
}
