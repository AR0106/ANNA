using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ANNA.BasicCommands
{
    public class Dictionary
    {
        public static string[] GetDefinition(string word, string language)
        {
            string apiResult = new WebClient().DownloadString("https://api.dictionaryapi.dev/api/v2/entries/" + language + '/' + word);

            // Cleanup
            apiResult = apiResult.Remove(0, 1);
            apiResult = apiResult.Remove(apiResult.Length - 1, 1);

            JObject apiDef = JObject.Parse(apiResult);

            JArray meanings = JArray.Parse(apiDef["meanings"].ToString());

            List<string> children = new List<string>();

            foreach (var meaning in meanings.Children())
            {
                JArray definitions = JArray.Parse(meaning["definitions"].ToString());
                foreach (var definition in definitions.Children())
                {
                    children.Add(definition["definition"].ToString());
                }
            }

            return children.ToArray();
        }
    }
}
