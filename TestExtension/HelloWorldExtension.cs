using ANNA;
using System;

namespace TestExtension
{
    public class HelloWordExtension : IAnnaExtension
    {
        public string[] ExampleInitSentences()
        {
            string[] values = { "for freedom!" };

            return values;
        }

        public void OnRun()
        {
            Console.WriteLine("Goodbye Mr. Stark");
        }

        public string[] SingleWordActions()
        {
            string[] values = { "freedom" };

            return values;
        }

        public Guid UUID()
        {
            return Guid.NewGuid();
        }

        Extension IAnnaExtension.AnnaExtension()
        {
            throw new NotImplementedException();
        }
    }
}
