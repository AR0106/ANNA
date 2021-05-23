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
            Console.WriteLine(Output.Out[Output.Out.Count - 1].response);
        }
    }
}
