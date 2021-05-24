using System;
using ANNA;
using ANNA.Interaction;

namespace ANNATests
{
    class Program
    {
        static void Main(string[] args)
        {
            ANNA.BasicCommands.Time.GetTime();
            Console.WriteLine(Output.mostRecentResponse.ExtensionID);
            Console.WriteLine(Output.mostRecentResponse.responseID);
            Console.WriteLine(Output.mostRecentResponse.response);
            Console.WriteLine(Output.mostRecentResponseIndex);
        }
    }
}
