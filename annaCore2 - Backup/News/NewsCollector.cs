using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace annaCore2.News
{
    public class NewsCollector
    {
        public struct newsStory
        {
            public string topic;
            public string headline;
            public string link;
            public string source;
        }

        private static HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
        private static HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();

        public static void main()
        {
            foreach (var item in NyPostCollector())
            {
                Console.WriteLine($"Topic: { item.topic }");
                Console.WriteLine($"Headline: { item.headline }");
                Console.WriteLine($"Link: { item.link }");
                Console.WriteLine("--------------------------------------------------------------------------");
            }
        }

        /*private static IList<newsStory> abcNewsCollector()
        {
            doc = web.Load("https://abcnews.go.com");
            HtmlAgilityPack.HtmlNode storyList = doc.DocumentNode.SelectSingleNode("/html/body/div[4]/section/section/div[1]/div/article[1]/ul");
            HtmlAgilityPack.HtmlNodeCollection stories = (HtmlAgilityPack.HtmlNodeCollection)storyList.ChildNodes;
        }*/

        public static IList<newsStory> NyPostCollector()
        {
            doc = web.Load("https://www.nypost.com");
            HtmlAgilityPack.HtmlNode storyList = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[4]/div[2]/div/div/div[1]");
            HtmlAgilityPack.HtmlNodeCollection stories = (HtmlAgilityPack.HtmlNodeCollection)storyList.ChildNodes;
            var articles = stories.Elements("article");

            List<newsStory> data = new List<newsStory>();

            var dEnumerator = articles.GetEnumerator();

            Regex rx = new Regex(@"\t|\n");

            for (int i = 0; i < 5; i++)
            {
                dEnumerator.MoveNext();

                newsStory rawData = new newsStory();

                rawData.headline = rx.Replace(dEnumerator.Current.Element("div").InnerText, "");
                rawData.link = dEnumerator.Current.Element("a").Attributes["href"].Value;
                rawData.source = "New York Post";

                data.Add(rawData);
            }

            return data;
        }

        public static IList<newsStory> BreitbartCollector()
        {
            doc = web.Load("https://www.breitbart.com");
            HtmlAgilityPack.HtmlNode storyList = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div/div/section/section[2]");
            HtmlAgilityPack.HtmlNodeCollection stories = storyList.ChildNodes;

            List<newsStory> data = new List<newsStory>();

            foreach (var story in stories)
            {
                newsStory rawData = new newsStory();

                rawData.headline = story.FirstChild.InnerText;
                rawData.link = story.FirstChild.FirstChild.Attributes["href"].Value;
                rawData.source = "Breitbart";

                data.Add(rawData);
            }

            return data;
        }

        public static IList<newsStory> NbcCollector()
        {
            doc = web.Load("https://www.nbcnews.com");
            HtmlAgilityPack.HtmlNode storyList = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/div[2]/div[4]/div/div[3]/div[2]/div[1]/div[1]/div[1]/div/div/div[2]/div/ul");
            HtmlAgilityPack.HtmlNodeCollection stories = storyList.ChildNodes;

            List<newsStory> data = new List<newsStory>();

            foreach (var story in stories)
            {
                newsStory rawData = new newsStory();
                var children = story.ChildNodes;

                int counter = 0;

                foreach (var item in children)
                {
                    if (counter == 1)
                    {
                        rawData.topic = item.FirstChild.InnerText;
                        rawData.headline = item.LastChild.InnerText;
                        rawData.link = item.LastChild.Attributes["href"].Value;
                        rawData.source = "NBC News";
                    }
                    counter++;
                }

                data.Add(rawData);
            }
            return data;
        }
    }
}