using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ANNA.Interaction
{
    public class gptJOutput
    {
        public string model { get; set; }
        public double compute_time { get; set; }
        public string text { get; set; }
        public int token_max_length { get; set; }
        public float temperature { get; set; }
        public float top_p { get; set; }
        public string stop_squence { get; set; }
    }
    internal class gptJ
    {
        private static Dictionary<char, string> illicitChars = new Dictionary<char, string>
        {
            { ' ', "%20" },
            { '!', "%21" },
            { '"', "%22" },
            { '!', "%23" },
        };

        private static HttpClient client = new HttpClient();

        public static Response Infer(string context, int tokenMax, float temperature, float top_p)
        {
            // Create Packet Request
            context = context.Replace(" ", "%20");

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri("http://api.vicgalle.net:5000/generate?context=" + '"' + context + '"'
                + "&token_max_length=" + tokenMax
                + "&temperature=" + temperature
                + "&top_p=" + top_p));
            request.Headers.Add("accept", "application/json");

            // Get Packet
            Task<HttpResponseMessage> packet = client.SendAsync(request);
            packet.Wait();

            var result = packet.Result.Content.ReadAsStringAsync(); ;
            JObject item = JObject.Parse(result.Result);

            // Add and Return Result to Response List

            Output.PushResponse(new Response(JsonConvert.DeserializeObject<gptJOutput>(item.ToString())));

            return Output.MostRecentResponse;
        }

        public static Response Infer(string context, int tokenMax, double temperature, double top_p)
        {
            context = context.Replace(" ", "%20");

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri("http://api.vicgalle.net:5000/generate?context=" + '"' + context + '"'
                + "&token_max_length=" + tokenMax
                + "&temperature=" + temperature
                + "&top_p=" + top_p));
            request.Headers.Add("accept", "application/json");

            Task<HttpResponseMessage> packet = client.SendAsync(request);
            packet.Wait();

            var result = packet.Result.Content.ReadAsStringAsync(); ;

            JObject item = JObject.Parse(result.Result);

            Output.PushResponse(new Response(JsonConvert.DeserializeObject<gptJOutput>(item.ToString())));

            return Output.MostRecentResponse;
        }
    }
}
