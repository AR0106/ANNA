﻿using ANNA.Interaction;
using System;

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

            /*Output.SendCommand("search", new string[] { "microsoft" });
            ANNA.BasicCommands.SearchResponse? response = Output.mostRecentResponse.response as ANNA.BasicCommands.SearchResponse?;
            Console.WriteLine(response.Value.instantAnswer);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(response.Value.sourceLink);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(response.Value.sourceName);*/

            /*Output.SendCommand("hello", null);
            string response = Output.mostRecentResponse.response as string;
            Console.WriteLine(response);*/
        }
    }
}
