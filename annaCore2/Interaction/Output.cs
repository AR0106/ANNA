﻿using AnnaMLTools.General;
using AnnaMLTools.Keyword;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.TextToSpeech.v1;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Raylib_cs.Raylib;

namespace ANNA.Interaction
{
    public class Output
    {
        public static bool directInput = true;
        public static bool generatedOutput = false;

        // Program Response Output
        public static List<Response> Responses = new();

        public static int MostRecentResponseIndex
        {
            get => Responses.Count - 1;
        }

        public static Response MostRecentResponse
        {
            get => Responses[MostRecentResponseIndex];
        }

        // Audio Output
        internal static int Speak(string sentence)
        {
            try
            {
                // Initiate IBM Watson TTS Service
                IamAuthenticator ibmAuth = new(
                    apikey: "WpUJamCM8z2Q_-X5i283MIeXodlGY2vJahX7rasOW6ae"
                    );

                TextToSpeechService speechService = new(ibmAuth);
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

        public static void InitML()
        {
            Vectorizer.InitVectorizer("vectorsMed.bin");
        }

        struct MLOutput
        {
            public string word;
            public double frequency;
        }

        // Machine Learning Action Output
        public static string ProcessInput(string Input, int broadness, Extension extension)
        {
            if (directInput)
            {
                return Input;
            }

            // Default ML Behavior
            List<MLOutput> LFrequencies = new();

            List<Tokenizer.Sentence> extensionSentences = new();

            // Encodes Each Initiation Sentence From the Extension and Puts it into a Tokenized Sentence List
            foreach (var sentence in extension.ExampleInitSentences)
            {
                foreach (var item in Tokenizer.Encode(sentence))
                {
                    extensionSentences.Add(item);
                }
            }

            // Tokenizes the User Input
            var tokenized = Tokenizer.Encode(Input);
            
            foreach (var sentence in tokenized)
            {
                foreach (var word in sentence.words_noUseless)
                {
                    // Gets Frequency of Each Word in The Extension Initiation Sentences
                    LFrequencies.Add(new MLOutput { frequency = tfidf.GetTFIDF(word, sentence, extensionSentences.ToArray()),
                                                    word = word});

                    // Takes Each Meaningful Word in The provided User Sentences, Finds Related Terms, and Vectorizes each of them
                    var vectors = Vectorizer.Vectorize(sentence, broadness);

                    // Gets Frequency of Similar Words in the 
                    foreach (var vector in vectors)
                    {
                        LFrequencies.Add(new MLOutput
                        {
                            frequency = tfidf.GetTFIDF(vector.Representation.WordOrNull, sentence, extensionSentences.ToArray()),
                            word = vector.Representation.WordOrNull
                        });
                    }
                }
                
            }

            // Sorts Words from Most Frequent to Least Frequent
            var sortedFrequencies = LFrequencies.OrderByDescending(f => f.frequency);

            if (Program.developerMode)
            {
                return $"{sortedFrequencies.ElementAt(0).word}:{sortedFrequencies.ElementAt(0).frequency}";
            }

            // Returns Most Frequent Word
            return sortedFrequencies.ElementAt(0).word;
        }

        // Audio Output Wrapper
        public static int Say(string sentence)
        {
            return Speak(sentence);
        }

        // Send Command to ANNA to Execute Program
        public static void SendCommand(string command, string[] args)
        {
            try
            {
                Program.RunANNA(command, args);
            }
            catch (Exception e)
            {
                Responses.Add(new Response(e));
            }
        }
    }
}
