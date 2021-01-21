using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using NCalc;
using reforceLibCore;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IronOcr;

namespace reforce_annaBot
{
    internal class aps
    {
        private string[] numZNum()
        {
            string[] NUM_Z_N_INDEX = new string[10];
            NUM_Z_N_INDEX[0] = "0";
            NUM_Z_N_INDEX[1] = "1";
            NUM_Z_N_INDEX[2] = "2";
            NUM_Z_N_INDEX[3] = "3";
            NUM_Z_N_INDEX[4] = "4";
            NUM_Z_N_INDEX[5] = "5";
            NUM_Z_N_INDEX[6] = "6";
            NUM_Z_N_INDEX[7] = "7";
            NUM_Z_N_INDEX[8] = "8";
            NUM_Z_N_INDEX[9] = "9";
            return NUM_Z_N_INDEX;
        }

        private string[] mathExpressions()
        {
            string[] MATH_EXPRESSIONS_INDEX = new string[4];
            MATH_EXPRESSIONS_INDEX[0] = "+";
            MATH_EXPRESSIONS_INDEX[1] = "-";
            MATH_EXPRESSIONS_INDEX[2] = "*";
            MATH_EXPRESSIONS_INDEX[3] = "/";
            return MATH_EXPRESSIONS_INDEX;
        }

        private string webResultURL;

        public void startOCR(string FILE_PATH)
        {
            try
            {
                if (FILE_PATH.Contains(@"\"))
                {
                    AutoOcr ocr = new AutoOcr();
                    string ocrResult = ocr.Read(FILE_PATH).ToString();

                    Console.WriteLine(ocrResult);
                    Console.Read();

                    if (ocrResult.Contains(numZNum().ToString()) && ocrResult.Contains(mathExpressions().ToString()))
                    {
                        Expression e = new Expression(ocrResult);
                        if (!e.HasErrors())
                        {
                            Console.WriteLine(e.Evaluate());
                        }
                    }
                    else if (FILE_PATH == "abc123")
                    {
                        dictionaryQuery("philanthropist");
                    }
                    else
                    {
                        webQuery(ocrResult);
                    }
                }
                else
                {
                    if (numZNum().Any(FILE_PATH.Contains) && mathExpressions().Any(FILE_PATH.Contains))
                    {
                        NCalc.Expression e = new NCalc.Expression(FILE_PATH);
                        if (!e.HasErrors())
                        {
                            Console.WriteLine(e.Evaluate());
                        }
                        else
                        {
                            webQuery(FILE_PATH);
                        }
                    }
                    else if (FILE_PATH == "abc123")
                    {
                        dictionaryQuery("philanthropist");
                    }
                    else
                    {
                        webQuery(FILE_PATH);
                    }
                }
                Console.ReadLine();
            }
            catch (FileNotFoundException)
            {
                Console.Clear();
                Console.WriteLine("File Not Found");
                Program.Main();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " " + e.Source + " " + e.Data + " " + e.StackTrace + " " + e.TargetSite + " " + e.HResult);
            }
        }

        public async Task<string> getWebResults(WebSearchClient client, string query)
        {
            try
            {
                var webData = await client.Web.SearchAsync(query: query);
                Console.WriteLine("Searching for " + query);

                Console.WriteLine(Thread.CurrentThread.Name);

                if (webData?.WebPages?.Value?.Count > 0)
                {
                    // find the first web page
                    var firstWebPagesResult = webData.WebPages.Value.FirstOrDefault();

                    if (firstWebPagesResult != null)
                    {
                        Console.WriteLine("Webpage Results # {0}", webData.WebPages.Value.Count);
                        Console.WriteLine("First web page name: {0} ", firstWebPagesResult.Name);
                        Console.WriteLine("First web page URL: {0} ", firstWebPagesResult.Url);
                        webResultURL = firstWebPagesResult.Url;
                    }
                    else
                    {
                        Console.WriteLine("Didn't find any web pages...");
                    }
                }
                else
                {
                    Console.WriteLine("Didn't find any web pages...");
                }

                /*
                 * Images
                 * If the search response contains images, the first result's name
                 * and url are printed.
                 */
                if (webData?.Images?.Value?.Count > 0)
                {
                    // find the first image result
                    var firstImageResult = webData.Images.Value.FirstOrDefault();

                    if (firstImageResult != null)
                    {
                        Console.WriteLine("Image Results # {0}", webData.Images.Value.Count);
                        Console.WriteLine("First Image result name: {0} ", firstImageResult.Name);
                        Console.WriteLine("First Image result URL: {0} ", firstImageResult.ContentUrl);
                    }
                    else
                    {
                        Console.WriteLine("Didn't find any images...");
                    }
                }
                else
                {
                    Console.WriteLine("Didn't find any images...");
                }

                /*
                 * News
                 * If the search response contains news articles, the first result's name
                 * and url are printed.
                 */
                if (webData?.News?.Value?.Count > 0)
                {
                    // find the first news result
                    var firstNewsResult = webData.News.Value.FirstOrDefault();

                    if (firstNewsResult != null)
                    {
                        Console.WriteLine("\r\nNews Results # {0}", webData.News.Value.Count);
                        Console.WriteLine("First news result name: {0} ", firstNewsResult.Name);
                        Console.WriteLine("First news result URL: {0} ", firstNewsResult.Url);
                    }
                    else
                    {
                        Console.WriteLine("Didn't find any news articles...");
                    }
                }
                else
                {
                    Console.WriteLine("Didn't find any news articles...");
                }

                /*
                 * Videos
                 * If the search response contains videos, the first result's name
                 * and url are printed.
                 */
                if (webData?.Videos?.Value?.Count > 0)
                {
                    // find the first video result
                    var firstVideoResult = webData.Videos.Value.FirstOrDefault();

                    if (firstVideoResult != null)
                    {
                        Console.WriteLine("\r\nVideo Results # {0}", webData.Videos.Value.Count);
                        Console.WriteLine("First Video result name: {0} ", firstVideoResult.Name);
                        Console.WriteLine("First Video result URL: {0} ", firstVideoResult.ContentUrl);
                    }
                    else
                    {
                        Console.WriteLine("Didn't find any videos...");
                    }
                }
                else
                {
                    Console.WriteLine("Didn't find any videos...");
                }
                return webResultURL;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encountered exception. " + ex.Message);
                return "error";
            }
        }

        public async void webQuery(string queryQuestion)
        {
            var client = new WebSearchClient(new ApiKeyServiceClientCredentials("2aa94bb1e78c4d23a8abf2ed87045209"));

            // Waits on the Results of the Web Search
            await getWebResults(client, queryQuestion);

            // Gets HTML of the First Website
            HttpWebRequest webAPSRequest = (HttpWebRequest)WebRequest.Create(webResultURL);
            webAPSRequest.Method = "GET";
            WebResponse webAPSResponse = webAPSRequest.GetResponse();
            StreamReader sr = new StreamReader(webAPSResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            webAPSResponse.Close();

            string.CompareOrdinal(queryQuestion, result);

            // Writes Only the Text From the HTML
            Console.WriteLine(WebFormatter.StripAll(result));
            Console.ReadKey();
        }

        /*
            0 = Oxford
            1 = Longman
            2 = Cambridge
            3 = Webster
            4 = Collins
            https://github.com/kiasar/Dictionary_crawler
        */

        private int dictionaryServiceKey = Properties.Settings.Default.dictionaryService;

        public void dictionaryQuery(string defineWord)
        {
            // Gets the HTML from the API
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://dictionaryapi.com/api/v3/references/collegiate/json/philanthropist?key=8fa8d3f4-d8f0-485e-971b-4bbf6712626d");
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();
            // Writes Only the Text From The HTML
            //Console.WriteLine(JsonConvert.DeserializeObject(WebFormatter.StripAll(result)));
            string definition;
            definition = null;
            dynamic dynObj = JsonConvert.DeserializeObject(WebFormatter.StripAll(result));
            Console.ReadLine();
        }
    }
}