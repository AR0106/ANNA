using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.TextToSpeech.v1;
using Newtonsoft.Json;
using Raylib_cs;
using Reforce_annaBotML.Model;
using System;
using System.Linq;
using System.IO;

using static Raylib_cs.Raylib;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;

namespace ANNA
{
    internal class Program
    {
        public static string inputType()
        {
            return Console.ReadLine().ToLower();
        }

        private static bool developerMode = true;
        private static bool directInput = true;

        public static string UserInput()
        {
            // Direct Input of ML Return Values
            if (directInput)
            {
                return Console.ReadLine().ToLower();
            }

            // Default ML Behavior
            else
            {
                ModelInput input = new ModelInput
                {
                    PHRASE = inputType()
                };

                if (developerMode)
                {
                    return $"{ConsumeModel.Predict(input).Prediction}:{ConsumeModel.Predict(input).Score[0]}";
                }

                return ConsumeModel.Predict(input).Prediction;
            }
        }

        private static string[] builtinCommands = { "hello", "world", "install"};

        public static int AnnaSay(string voice, string sentence)
        {
            try
            {
                // Initiate IBM Watson TTS Service
                IamAuthenticator ibmAuth = new IamAuthenticator(
                    apikey: "WpUJamCM8z2Q_-X5i283MIeXodlGY2vJahX7rasOW6ae"
                    );

                TextToSpeechService speechService = new TextToSpeechService(ibmAuth);
                speechService.SetServiceUrl("https://api.us-south.text-to-speech.watson.cloud.ibm.com/instances/bbfeff9a-0fa9-48db-9ee5-4a291306bc0a");

                var speechResult = speechService.Synthesize(sentence, "audio/wav", voice);
                
                // Write TTS Service Return Bytes to ".wav" File
                using (FileStream stream = File.Create("anna.wav"))
                {
                    speechResult.Result.WriteTo(stream);
                    stream.Close();
                    speechResult.Result.Close();
                }

                // Initiate Audio Service
                InitAudioDevice();
                Music audio = LoadMusicStream("anna.wav");
                PlayMusicStream(audio);

                float i = GetMusicTimeLength(audio) / 30000;
                float j = GetMusicTimePlayed(audio);
                float previousTimePlayed = 0;

                // Play The Audio File
                while (GetMusicTimePlayed(audio) <= i)
                {
                    previousTimePlayed = GetMusicTimePlayed(audio);
                    UpdateMusicStream(audio);
                }

                UnloadMusicStream(audio);

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.InnerException);

                return 1;
            }

        }

        internal static int InstallExtension()
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

        private static void Main(string[] args)
        {
            // DEV DEBUG ONLY
            if (directInput)
            {
                Console.WriteLine("Direct Input Mode");
            }

            string refInput = UserInput();

            // Built-In Commands
            if (builtinCommands.Any(refInput.Contains))
            {
                switch (refInput)
                {
                    case "hello":
                        AnnaSay("en-US_AllisonV3Voice", "Hi! I'm Anna!");
                        return;

                    case "world":
                        AnnaSay("en-US_AllisonV3Voice", "I'm on Earth! What world are you on?");
                        return;

                    case "install":
                        InstallExtension();
                        return;
                }
            }

            // Modules
            else
            {
                if (!Directory.Exists("extensions"))
                {
                    Directory.CreateDirectory("extensions");
                }

                foreach (var extension in Directory.EnumerateFiles("extensions", "*.dll", SearchOption.AllDirectories))
                {
                    var assembly = Assembly.LoadFrom(extension);

                    var extensionTypes = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => typeof(IAnnaExtension).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

                    // Instatiates Each Extension
                    foreach (var extensionType in extensionTypes)
                    {
                        var extInstance = (IAnnaExtension)Activator.CreateInstance(extensionType);

                        // Checks if it is the Correct Command
                        if (extInstance.SingleWordActions().Any(refInput.Contains))
                        {
                            // Runs Extension
                            extInstance.OnRun();
                        }
                    }
                }
            }
        }
    }
}