using AnnaMLTools.General;
using System;
using System.Collections.Generic;
using Word2vec.Tools;

namespace AnnaMLTools.Keyword
{

    [Serializable]
    public class VectorizerNotInitialized : Exception
    {
        public override string Message
        {
            get
            {
                return "The Vectorizer Has Not Been Initialized. If You are Using ANNA Call 'Output.InitML()'. If You are Using AnnaMLTools Call 'Vectorizer.InitVectorizer'.";
            }
        }
    }
    public class Vectorizer
    {
        private static bool isVectorizerInit = false;

        private static Vocabulary vocab;

        public static void InitVectorizer()
        {
            vocab = new Word2VecBinaryReader().Read("vectorsMed.bin");
            isVectorizerInit = true;
        }

        public static DistanceTo[] Vectorize(Tokenizer.Sentence sentence, int numOfClosestWords)
        {
            if (!isVectorizerInit)
            {
                throw new VectorizerNotInitialized();
            }
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
