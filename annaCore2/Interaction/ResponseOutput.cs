using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.TextToSpeech.v1;
using Raylib_cs;
using Reforce_annaBotML.Model;
using System;
using System.IO;
using ANNA;

using static Raylib_cs.Raylib;

namespace ANNA.Interaction
{
    public class Output
    {
        public static bool directInput = true;

        // Machine Learning Action Output
        public static string ProcessInput(string Input)
        {
            if (directInput)
            {
                return Input;
            }

            // Default ML Behavior
            ModelInput input = new ModelInput
            {
                PHRASE = Input
            };

            if (Program.developerMode)
            {
                return $"{ConsumeModel.Predict(input).Prediction}:{ConsumeModel.Predict(input).Score[0]}";
            }

            return ConsumeModel.Predict(input).Prediction;
        }

        // Audio Output
        public static int Speak(string sentence)
        {
            try
            {
                // Initiate IBM Watson TTS Service
                IamAuthenticator ibmAuth = new IamAuthenticator(
                    apikey: "WpUJamCM8z2Q_-X5i283MIeXodlGY2vJahX7rasOW6ae"
                    );

                TextToSpeechService speechService = new TextToSpeechService(ibmAuth);
                speechService.SetServiceUrl("https://api.us-south.text-to-speech.watson.cloud.ibm.com/instances/bbfeff9a-0fa9-48db-9ee5-4a291306bc0a");

                var speechResult = speechService.Synthesize(sentence, "audio/wav", "en-US_AllisonV3Voice");

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

                // Play The Audio File
                while (GetMusicTimePlayed(audio) <= i)
                {
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

        // Send Command to ANNA to Execute Program
        public void SendCommand(string command)
        {
            Program.RunANNA(command);
        }
    }
}
