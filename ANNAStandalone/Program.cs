using ANNA;
using ANNA.Interaction;
using System;
using System.Threading;

namespace ANNAStandalone
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Output.Voice();
            Thread.Sleep(1000);
            if (Output.MostRecentResponseIndex > -1)
            {
                Console.WriteLine((string)Output.MostRecentResponse.response);
            }
            else
            {
                Console.Write("I'm Sorry, I didn't get that");
            }
            Console.ReadLine();
        }
    }
}
