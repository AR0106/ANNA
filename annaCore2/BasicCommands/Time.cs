using System;
using ANNA.Interaction;

namespace ANNA.BasicCommands
{
    public class Time
    {
        public struct TimeLayout
        {
            public int millisecond;
            public int second;
            public int minute;
            public int hour;
            public int dayOfMonth;
            public string dayOfWeek;
            public string month;
            public int monthNumber;
            public int year;
            public int unixTimestamp;
        }

        public static void GetTime()
        {
            DateTime time = new DateTime();

            Response timeResponse = new Response(time.DayOfWeek.ToString());
            Output.Out.Add(timeResponse);
        }
    }
}
