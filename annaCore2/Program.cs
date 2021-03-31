using Reforce_annaBotML.Model;
using System;
using System.Linq;
using IBM.Watson.TextToSpeech.v1;
using System.IO;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using System.Threading;
using System.Runtime.InteropServices;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace annaCore2
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

        public static int AnnaSay(string voice, string saying)
        {
            try
            {
                IamAuthenticator ibmAuth = new IamAuthenticator(
                    apikey: "WpUJamCM8z2Q_-X5i283MIeXodlGY2vJahX7rasOW6ae"
                    );

                TextToSpeechService speechService = new TextToSpeechService(ibmAuth);
                speechService.SetServiceUrl("https://api.us-south.text-to-speech.watson.cloud.ibm.com/instances/bbfeff9a-0fa9-48db-9ee5-4a291306bc0a");

                var voices = speechService.ListVoices();
                Console.WriteLine(voices.Response);

                var speechResult = speechService.Synthesize(saying, "audio/wav", voice);
                using (FileStream stream = File.Create("anna.wav"))
                {
                    speechResult.Result.WriteTo(stream);
                    stream.Close();
                    speechResult.Result.Close();
                }

                InitAudioDevice();
                Music audio = LoadMusicStream("anna.wav");
                PlayMusicStream(audio);

                float i = GetMusicTimeLength(audio);
                float j = GetMusicTimePlayed(audio);
                float previousTimePlayed = 0;

                int repeatCounter = 0;

                Console.WriteLine(i);

                while (GetMusicTimePlayed(audio) <= i)
                {
                    if (GetMusicTimePlayed(audio) == previousTimePlayed)
                    {
                        repeatCounter++;
                        if (repeatCounter == 1100)
                        {
                            break;
                        }
                    }
                    Console.WriteLine(GetMusicTimePlayed(audio));
                    Console.WriteLine(i);
                    Console.WriteLine(repeatCounter);
                    previousTimePlayed = GetMusicTimePlayed(audio);
                    UpdateMusicStream(audio);
                }

                return 0;
            }
            catch (Exception e)
            {
                return 1;
            }

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
                        AnnaSay("en-US_AllisonV3Voice", "I'm on Earth!");
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