using ANNA.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace ANNATests
{
    class Program
    {
        private class Person
        {
            public string name { get; set; }
            public int age { get; set; }
            public List<string> interests { get; set; }
        }

        static void Main(string[] args)
        {
            /*ANNA.BasicCommands.Time.GetTime();
            Console.WriteLine(Output.MostRecentResponse.ExtensionID);
            Console.WriteLine(Output.MostRecentResponse.responseID);
            Console.WriteLine(Output.MostRecentResponse.response);
            Console.WriteLine(Output.MostRecentResponseIndex);

            Extension extension = new Extension();
            extension.Name = "Anna Test Extension";
            extension.Author = "Reforce Labs";
            extension.Published = DateTime.Now;
            extension.Link = null;

            Console.WriteLine(extension.ANEID);
            Output.SendCommand("ANEID");*/

            /*Output.SendCommand("search", new string[] { "microsoft" });
            ANNA.BasicCommands.SearchResponse? response = Output.MostRecentResponse.response as ANNA.BasicCommands.SearchResponse?;
            Console.WriteLine(response.Value.instantAnswer);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(response.Value.sourceLink);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(response.Value.sourceName);*/

            /*Output.SendCommand("hello", null);
            string response = Output.MostRecentResponse.response as string;
            Console.WriteLine(response);*/

            /*List<Person> people = new();
            people.Add(new Person { name = "John", age = 15, interests = new List<string> { "sports", "cars", "bodybuilding" } });
            people.Add(new Person { name = "Harry", age = 7, interests = new List<string> { "cars", "firefighting", "policing" } });
            people.Add(new Person { name = "Carlos", age = 36, interests = new List<string> { "construction", "politics", "movies" } });
            people.Add(new Person { name = "Joel", age = 22, interests = new List<string> { "women", "technology", "stocks" } });
            people.Add(new Person { name = "Katelin", age = 12, interests = new List<string> { "makeup", "women", "law" } });

            //Person[] people = new Person[5];
            //people[0] = new Person { name = "John", age = 15, interests = new List<string> { "sports", "cars", "bodybuilding" } };
            //people[1] = new Person { name = "Harry", age = 7, interests = new List<string> { "cars", "firefighting", "policing" } };
            //people[2] = new Person { name = "Carlos", age = 36, interests = new List<string> { "construction", "politics", "movies" } };
            //people[3] = new Person { name = "Joel", age = 22, interests = new List<string> { "women", "technology", "stocks" } };
            //people[4] = new Person { name = "Katelin", age = 12, interests = new List<string> { "makeup", "women", "law" } };

            foreach (var person in people)
            {
                person.interests.Sort();
            }

            var test = ANNA.Data.DataSorting.Sort(people, "age");*/

            ANNA.BasicCommands.Dictionary.GetDefinition("general", "en_US");
        }
    }
}
