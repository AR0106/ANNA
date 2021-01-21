using Reforce_annaBotML.Model;
using System;
using System.Linq;
using IronPython;
using System.IO;

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

        private static string[] builtinCommands = { "hello", "world", "news", "weather" };

        public static void AnnaSay(int rate, double volume, int voice, string saying)
        {
            StreamReader ttsReader = new StreamReader("tts.py");
            var ttsCode = ttsReader.ReadToEnd();
            ttsReader.Close();

            var engine = IronPython.Hosting.Python.CreateEngine();
            var scope = engine.CreateScope();
            engine.Execute(ttsCode);

            var _annaSay = scope.GetVariable("annaSay");
            var result = _annaSay(rate, volume, voice, saying);
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
                        AnnaSay(170, 1.0, 1, "Hello, User!");
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
                }
            }

            // Modules
            else
            {
            }
        }
    }
}