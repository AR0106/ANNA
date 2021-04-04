using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANNA
{
    public interface IAnnaExtension
        {
            void OnRun();

            Guid UUID();

            string[] SingleWordActions();

            string[] ExampleInitSentences();
        }

    public struct Extension
    {
        public string Name;
        public string Author;
        public DateTime Published;
        public Guid UUID;
        public string[] SingleWordActions;
        public string[] ExampleInitSentences;
    }

    public class AnnaTestExtension : IAnnaExtension
    {
        public string[] ExampleInitSentences()
        {
            string[] sentences = new string[1];

            sentences[0] = "open my extension";

            return sentences;
        }

        public void OnRun()
        {
            Console.WriteLine("This is my Extension");
        }

        public string[] SingleWordActions()
        {
            string[] words = new string[1];

            words[0] = "extension";

            return words;
        }

        public Guid UUID()
        {
            return Guid.NewGuid();
        }
    }
}
