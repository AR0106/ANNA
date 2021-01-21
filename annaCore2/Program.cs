using Reforce_annaBotML.Model;
using System;
using System.Linq;
using IBM.Watson.TextToSpeech.v1;
using System.IO;
using IBM.Cloud.SDK.Core.Authentication.Iam;

namespace annaCore2
{
    internal class Program
    {
        public static string inputType()
        {
            return Console.ReadLine().ToLower();
        }

        private static bool developerMode = false;
        private static bool directInput = false;

        public static string UserInput()
        {
            // Direct Input
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

        private static string[] builtinCommands = { "hello", "world", "news", "weather", "aps" };

        public static void AnnaSay(string voice, string saying)
        {
            IamAuthenticator ibmAuth = new IamAuthenticator(
                apikey: "WpUJamCM8z2Q_-X5i283MIeXodlGY2vJahX7rasOW6ae"
                );

            TextToSpeechService speechService = new TextToSpeechService(ibmAuth);
            speechService.SetServiceUrl("https://api.us-south.text-to-speech.watson.cloud.ibm.com/instances/bbfeff9a-0fa9-48db-9ee5-4a291306bc0a");

            var voices = speechService.ListVoices();
            Console.WriteLine(voices.Response);

            var speechResult = speechService.Synthesize(saying, "audio/wav", voice);
            using (FileStream stream = File.Create(saying + ".wav"))
            {
                speechResult.Result.WriteTo(stream);
                stream.Close();
                speechResult.Result.Close();
            }

            SoundPlayer
        }

        private static void Main(string[] args)
        {
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
                        Console.WriteLine("I'm on Earth!");
                        return;

                    case "news":
                        News.NewsCollector.main();
                        return;

                    case "weather":
                        Weather.WeatherCollector.main();
                        return;
                    case "aps":
                        AnnaSay("en-US_AllisonV3Voice", "Hi! I'm Anna!");
                        return;
                }
            }

            // Modules
            else
            {
            }
        }
    }
}