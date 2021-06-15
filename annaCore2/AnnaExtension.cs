using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace ANNA
{
    public interface IAnnaExtension
    {
        dynamic OnRun(string[] args);

        Extension AnnaExtension();

        string[] SingleWordActions();

        string[] ExampleInitSentences();
    }

    public class Extension
    {
        public string Name
        {
            get; set;
        }

        public string Author
        {
            get; set;
        }

        public DateTime Published
        {
            get; set;
        }

        public string ANEID // AN(ANNA) E(Extension) ID
        {
            get => (Author.Length * 1024).ToString() + (ExtName.Length * 2048).ToString() + String.Concat(($"_{Author}.{Name}_".ToLower()).Where(c => !Char.IsWhiteSpace(c))) + Published.Ticks / 12;
        }

        public Uri Link
        {
            get; set;
        }

        public string ExtName
        {
            get => String.Concat(Name.Trim().ToLower().Where(c => !Char.IsWhiteSpace(c)));
        }

        public string[] SingleWordActions
        {
            get; set;
        }

        public string[] ExampleInitSentences
        {
            get; set;
        }
    }

    internal class Extensions
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
                File.WriteAllBytes(extension.ANEID.ToString() + ".zip", data);
            }

            System.IO.Compression.ZipFile.ExtractToDirectory(extension.ANEID.ToString() + ".zip", Environment.CurrentDirectory + "/extensions/" + extension.ANEID.ToString());

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
                Link = extension.Link,
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

        public dynamic OnRun(string[] args)
        {
            Console.WriteLine("This is my Extension");
            return null;
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
                Link = null,
                SingleWordActions = SingleWordActions(),
                ExampleInitSentences = ExampleInitSentences()
            };
        }
    }
}
