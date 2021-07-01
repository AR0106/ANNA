using System;
using System.Collections.Generic;
using Word2vec.Tools;

namespace AnnaMLTools.PreTraining
{
    public class Vectorizer
    {
        private static Vocabulary vocab;

        public static void InitVectorizer()
        {
            vocab = new Word2VecBinaryReader().Read("vectorsMed.bin");
        }

        public static DistanceTo[] Vectorize(Tokenizer.Sentence sentence, int numOfClosestWords)
        {
            List<DistanceTo> LDistances = new List<DistanceTo>();
            foreach (var word in sentence.words_noUseless)
            {
                var closest = vocab.Distance(word, numOfClosestWords);

                foreach (var item in closest)
                {
                    LDistances.Add(item);
                }
            }

            return LDistances.ToArray();
        }
    }
}
