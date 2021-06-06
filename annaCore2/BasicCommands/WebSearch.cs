using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace ANNA.BasicCommands
{
    public struct SearchResponse
    {
        public string instantAnswer;
        public string sourceName;
        public Uri sourceLink;
    };
    public class WebSearch
    {
        public static SearchResponse RunSearch(string searchTopic)
        {
            using var client = new WebClient();
            JObject content = JObject.Parse(client.DownloadString("https://api.duckduckgo.com/?q=" + searchTopic.Replace(' ', '+') + "&format=json&pretty=1&t=reforceanna"));

            return new SearchResponse
            {
                instantAnswer = content["Abstract"].ToString(),
                sourceName = content["AbstractSource"].ToString(),
                sourceLink = new Uri(content["AbstractURL"].ToString())
            };
        } 
    }
}
