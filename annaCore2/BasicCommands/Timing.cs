using ANNA.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ANNA.BasicCommands
{
    public class Timing
    {
        private static Timer timer;
        public static void StartTimer(float seconds)
        {
            timer = new Timer(seconds * 1000);
            timer.Start();
            timer.Elapsed += TimerEnded;
        }

        private static void TimerEnded(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            Output.Responses.Add(new Response($"The Elapsed event was raised at {e.SignalTime}"));
        }
    }
}
