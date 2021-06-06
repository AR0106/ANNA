using System;
using ANNA;
using ANNA.Interaction;

namespace ANNATests
{
    class Program
    {
        static void Main(string[] args)
        {
            /*ANNA.BasicCommands.Time.GetTime();
            Console.WriteLine(Output.mostRecentResponse.ExtensionID);
            Console.WriteLine(Output.mostRecentResponse.responseID);
            Console.WriteLine(Output.mostRecentResponse.response);
            Console.WriteLine(Output.mostRecentResponseIndex);

            Extension extension = new Extension();
            extension.Name = "Anna Test Extension";
            extension.Author = "Reforce Labs";
            extension.Published = DateTime.Now;
            extension.Link = null;

            Console.WriteLine(extension.ANEID);
            Output.SendCommand("ANEID");*/

            dynamic output;
            Output.SendCommand("search", new string[] { "microsoft" }, out output);
            Nullable<ANNA.BasicCommands.SearchResponse> response = output as Nullable<ANNA.BasicCommands.SearchResponse>;
            Console.WriteLine(response.Value.instantAnswer);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(response.Value.sourceLink);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(response.Value.sourceName);
        }
    }
}
