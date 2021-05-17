using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ANNA
{
    public interface IAnnaExtension
    {
        void OnRun();

        Extension AnnaExtension();

        string[] SingleWordActions();

        string[] ExampleInitSentences();
    }

    public struct Extension
    {
        public string Name;
        public string Author;
        public DateTime Published;
        public Guid UUID;
        public Uri Link;
        public string ExtName;
        public string[] SingleWordActions;
        public string[] ExampleInitSentences;
    }

    public class Extensions
    {
        public static int InstallExtension(Extension extension)
        {
            //try
            //{
            if (!File.Exists("extensions.json"))
            {
                FileStream extensionsFile = File.Create("extensions.json");
                extensionsFile.Close();
            }

            using (WebClient client = new WebClient())
            {
                byte[] data = client.DownloadData(extension.Link);
                File.WriteAllBytes(extension.UUID.ToString() + ".zip", data);
            }

            System.IO.Compression.ZipFile.ExtractToDirectory(extension.UUID.ToString() + ".zip", Environment.CurrentDirectory + "/extensions/" + extension.UUID.ToString());

            var interfaceType = typeof(IAnnaExtension);
            var all = AppDomain.CurrentDomain.GetAssemblies()
              .SelectMany(x => x.GetTypes())
              .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
              .Select(x => Activator.CreateInstance(x));

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.None;

            Extension ext = new Extension
            {
                Name = extension.Name,
                Author = extension.Author,
                Published = extension.Published,
                UUID = extension.UUID,
                Link = extension.Link,
                ExtName = extension.ExtName,
                SingleWordActions = extension.SingleWordActions,
                ExampleInitSentences = extension.ExampleInitSentences
            };

                var json = JsonConvert.SerializeObject(ext, Formatting.Indented);

                File.AppendAllText("extensions.json", json);



            return 0;
            /*}
            catch (Exception e)
            {
                throw;
            }*/
        }
    }

    public class AnnaTestExtension : IAnnaExtension
    {
        public string[] ExampleInitSentences()
        {
            string[] sentences = new string[1];

            sentences[0] = "open my extension";

            return sentences;
        }

        public void OnRun()
        {
            Console.WriteLine("This is my Extension");
        }

        public string[] SingleWordActions()
        {
            string[] words = new string[1];

            words[0] = "extension";

            return words;
        }

        public Extension AnnaExtension()
        {
            return new Extension
            {
                Name = "Anna Test Extension",
                Author = "Reforce Labs",
                Published = DateTime.Now,
                UUID = Guid.NewGuid(),
                Link = null,
                ExtName = "anna-test-extension",
                SingleWordActions = SingleWordActions(),
                ExampleInitSentences = ExampleInitSentences()
            };
        }
    }
}
