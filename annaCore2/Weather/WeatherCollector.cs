using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace annaCore2.Weather
{
    public class WeatherCollector
    {
        private static HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        private static HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();

        public struct weatherData
        {
            public string time;
            public string highTemp;
            public string lowTemp;
            public string currentTemp;
            public string percip;
        }

        public static void main()
        {
            foreach (var data in TodayDataCollector())
            {
                Console.WriteLine(data.time.ToUpper());
                Console.WriteLine($"High Temp: { data.highTemp }");
                Console.WriteLine($"Percipitation: { data.percip }");
                Console.WriteLine("--------------------------------------------");
            }

            /*foreach (var data in DailyDataCollector())
            {
                Console.WriteLine(data.time.ToUpper());
                Console.WriteLine($"High Temp: { data.highTemp }");
                Console.WriteLine($"Low Temp: { data.lowTemp }");
                Console.WriteLine($"Percipitation: { data.percip }");
                Console.WriteLine("--------------------------------------------");
            }*/

            /*foreach (var data in HourlyDataCollector())
            {
                Console.WriteLine(data.time.ToUpper());
                Console.WriteLine($"Temp: { data.currentTemp }");
                Console.WriteLine($"Percipitation: { data.percip }");
                Console.WriteLine("--------------------------------------------");
            }*/
        }

        public static IList<weatherData> HourlyDataCollector()
        {
            // Loads the Weather Channel Website
            doc = web.Load("https://weather.com/weather/today");
            HtmlNode hourly = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/main/div[2]/div[2]/div[7]/section/div/ul");
            HtmlNodeCollection hourlyValues = hourly.ChildNodes;

            List<weatherData> data = new List<weatherData>();

            foreach (var item in hourlyValues)
            {
                weatherData rawData = new weatherData();
                var children = item.ChildNodes;

                foreach (var child in children)
                {
                    // Retrieves the Time from The Header on the Card
                    var wTime = child.Element("h3");
                    rawData.time = wTime.InnerText;

                    // Retrieves The Temperature and Percipitation
                    var divs = child.Elements("div");

                    var dEnumerator = divs.GetEnumerator();

                    int counter = 0;

                    for (int i = 0; i < 3; i++)
                    {
                        dEnumerator.MoveNext();
                        // If The Data is On the Temperature
                        if (counter == 0)
                        {
                            rawData.currentTemp = dEnumerator.Current.InnerText;
                        }
                        // If The Data is On the Perciptation
                        else if (counter == 2)
                        {
                            rawData.percip = dEnumerator.Current.InnerText;
                        }
                        counter++;
                    }
                }
                data.Add(rawData);
            }

            return data;
        }

        public static IList<weatherData> DailyDataCollector()
        {
            // Loads the Weather Channel Website
            doc = web.Load("https://weather.com/weather/today");
            HtmlNode hourly = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/main/div[2]/div[2]/div[10]/section/div/ul");
            HtmlNodeCollection dailyValues = hourly.ChildNodes;

            List<weatherData> data = new List<weatherData>();

            foreach (var item in dailyValues)
            {
                weatherData rawData = new weatherData();
                var children = item.ChildNodes;

                foreach (var child in children)
                {
                    // Retrieves the Time from The Header on the Card
                    var wTime = child.Element("h3");
                    rawData.time = wTime.InnerText;

                    // Retrieves The Temperature and Percipitation
                    var divs = child.Elements("div");

                    var dEnumerator = divs.GetEnumerator();

                    int counter = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        dEnumerator.MoveNext();
                        // If The Data is On the High Temperature
                        if (counter == 0)
                        {
                            rawData.highTemp = dEnumerator.Current.InnerText;
                        }
                        // If The Data is On the Perciptation
                        else if (counter == 3)
                        {
                            rawData.percip = dEnumerator.Current.InnerText;
                        }
                        counter++;
                    }
                }
                data.Add(rawData);
            }

            return data;
        }

        public static IList<weatherData> TodayDataCollector()
        {
            // Loads the Weather Channel Website
            doc = web.Load("https://weather.com/weather/today");
            HtmlNode hourly = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/main/div[2]/div[2]/div[2]/section/div/ul");
            HtmlNodeCollection dailyValues = hourly.ChildNodes;

            List<weatherData> data = new List<weatherData>();

            foreach (var item in dailyValues)
            {
                weatherData rawData = new weatherData();
                var children = item.ChildNodes;

                foreach (var child in children)
                {
                    // Retrieves the Time from The Header on the Card
                    var wTime = child.Element("h3");
                    rawData.time = wTime.InnerText;

                    // Retrieves The Temperature and Percipitation
                    var divs = child.Elements("div");

                    var dEnumerator = divs.GetEnumerator();

                    int counter = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        dEnumerator.MoveNext();
                        // If The Data is On the High Temperature
                        if (counter == 0)
                        {
                            rawData.highTemp = dEnumerator.Current.InnerText;
                        }
                        // If The Data is On the Low Temperature
                        else if (counter == 1)
                        {
                            rawData.lowTemp = dEnumerator.Current.InnerText;
                        }
                        // If The Data is On the Perciptation
                        else if (counter == 2)
                        {
                            rawData.percip = dEnumerator.Current.InnerText;
                        }
                        counter++;
                    }
                }
                data.Add(rawData);
            }

            return data;
        }
    }
}