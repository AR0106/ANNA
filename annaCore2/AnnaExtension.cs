using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANNA
{
    public interface IAnnaExtension
        {
            void OnRun();

            Guid UUID();

            string[] SingleWordActions();

            string[] ExampleInitSentences();
        }

    public struct Extension
    {
        public string Name;
        public string Author;
        public DateTime Published;
        public Guid UUID;
        public string[] SingleWordActions;
        public string[] ExampleInitSentences;
    }

    public class Extensions
    {
        public static int InstallExtension()
        {
            //try
            //{
            if (!File.Exists("extensions.json"))
            {
                FileStream extensionsFile = File.Create("extensions.json");
                extensionsFile.Close();
            }

            var interfaceType = typeof(IAnnaExtension);
            var all = AppDomain.CurrentDomain.GetAssemblies()
              .SelectMany(x => x.GetTypes())
              .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
              .Select(x => Activator.CreateInstance(x));

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.None;

            var extensionsList = all.ToList();

            DateTime pubDate = DateTime.Now;

            foreach (var item in extensionsList)
            {
                var inst = (IAnnaExtension)Activator.CreateInstance("annaCore2", item.ToString()).Unwrap();

                Extension ext = new Extension
                {
                    Name = "Test Extension",
                    Author = "Reforce Labs",
                    Published = pubDate,
                    UUID = inst.UUID(),
                    SingleWordActions = inst.SingleWordActions(),
                    ExampleInitSentences = inst.ExampleInitSentences()
                };

                var json = JsonConvert.SerializeObject(ext, Formatting.Indented);

                File.AppendAllText("extensions.json", json);
            }



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

        public Guid UUID()
        {
            return Guid.NewGuid();
        }
    }
}
