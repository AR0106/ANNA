using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace ANNA
{
    public interface IAnnaCommand
    {
        dynamic OnRun(string[] args);

        string[] SingleWordActions();

        string[] ExampleInitSentences();
    }

    public class Extension
    {
        public List<IAnnaCommand> Commands;

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
    }

    internal class Extensions
    {
        public static int InstallExtension(Extension extension)
        {
            if (!File.Exists("extensions.json"))
            {
                FileStream extensionsFile = File.Create("extensions.json");
                extensionsFile.Close();
            }

            using (WebClient client = new WebClient())
            {
                byte[] data = client.DownloadData(extension.Link);
                File.WriteAllBytes(extension.ANEID.ToString() + ".annaext", data);
            }

            System.IO.Compression.ZipFile.ExtractToDirectory(extension.ANEID.ToString() + ".zip", Environment.CurrentDirectory + "/extensions/" + extension.ANEID.ToString());

            var interfaceType = typeof(IAnnaCommand);
            var all = AppDomain.CurrentDomain.GetAssemblies()
              .SelectMany(x => x.GetTypes())
              .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
              .Select(x => Activator.CreateInstance(x));

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.None;

            Extension ext = new Extension()
            {
                Name = extension.Name,
                Author = extension.Author,
                Published = extension.Published,
                Link = extension.Link
            };

            var json = JsonConvert.SerializeObject(ext, Formatting.Indented);

            File.AppendAllText("extensions.json", json);

            return 0;
        }
    }
}
