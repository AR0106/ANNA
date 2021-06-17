using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANNA.BasicCommands
{
    internal class Greeting
    {
        static string[] greetings(string user)
        {
            string[] greetingsList = new string[4];
            greetingsList[0] = $"Hello {user}";
            greetingsList[1] = $"Hi {user}";
            greetingsList[2] = $"Greetings {user}";

            if (DateTime.Now.Hour < 12 && DateTime.Now.Hour <= 0)
                greetingsList[3] = $"Good Morning {user}";
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 20)
                greetingsList[3] = $"Good Afternoon {user}";
            else
                greetingsList[3] = $"Good Evening {user}";

            return greetingsList;
        }

        public static string GetGreeting(string user)
        {
            Random random = new Random();
            return greetings(user)[random.Next(0, 3)];
        }
    }
}
