using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using reforceLibCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace reforce_annaBot
{
    internal class fRecognition
    {
        //Scan Type
        public static string facialUser;

        public static string anger;
        public static string contempt;
        public static string disgust;
        public static string fear;
        public static string hapiness;
        public static string neutral;
        public static string sadness;
        public static string surprise;
        public static string glasses;
        public static string hair;
        public static string gender;
        public static string age;
        public static string smile;
        public static string accessories;
        public static string makeup;
        public static string exDisplay;

        // subscriptionKey = "0123456789abcdef0123456789ABCDEF"
        private const string subscriptionKey = "06ae31cd4a7041728dc823a081665c18";

        // Specify the Azure region
        private const string faceEndpoint =
            "https://westcentralus.api.cognitive.microsoft.com";

        // localImagePath = @"C:\Documents\LocalImage.jpg"
        private static string localImagePath = @"";

        private static string remoteImageUrl = "";

        private static readonly FaceAttributeType[] faceAttributes =
            {FaceAttributeType.Gender, FaceAttributeType.Age,
             FaceAttributeType.Smile, FaceAttributeType.Emotion,
             FaceAttributeType.Glasses, FaceAttributeType.Hair,
             FaceAttributeType.Accessories, FaceAttributeType.Makeup};

        public static int facialSaveNumber = 1;

        public static string GetFaceAttributes(IList<DetectedFace> faceList, string imagePath)
        {
            string attributes = string.Empty;
            StringBuilder sb = new StringBuilder();

            foreach (DetectedFace face in faceList)
            {
                gender = "gender: " + face.FaceAttributes.Gender.ToString();
                age = "age: " + face.FaceAttributes.Age.ToString();
                smile = "smile: " + face.FaceAttributes.Smile.ToString();
                accessories = "accessories: +" + face.FaceAttributes.Accessories.ToString();
                makeup = "makeup: " + face.FaceAttributes.Makeup.ToString();
                sb.Append("Face: ");

                // Add the gender, age, and smile.
                sb.Append(face.FaceAttributes.Gender);
                sb.Append(", ");
                sb.Append(face.FaceAttributes.Age);
                sb.Append(", ");
                /*sb.Append(face.FaceId);
                sb.Append(", ");*/
                sb.Append(string.Format("smile {0:F1}%, ", face.FaceAttributes.Smile * 100));

                // Add the emotions. Display all emotions over 10%.
                sb.Append("Emotion: ");
                Emotion emotionScores = face.FaceAttributes.Emotion;
                if (emotionScores.Anger >= 0.1f) sb.Append(
                    string.Format("anger {0:F1}%, ", emotionScores.Anger * 100));
                if (emotionScores.Contempt >= 0.1f) sb.Append(
                    string.Format("contempt {0:F1}%, ", emotionScores.Contempt * 100));
                if (emotionScores.Disgust >= 0.1f) sb.Append(
                    string.Format("disgust {0:F1}%, ", emotionScores.Disgust * 100));
                if (emotionScores.Fear >= 0.1f) sb.Append(
                    string.Format("fear {0:F1}%, ", emotionScores.Fear * 100));
                if (emotionScores.Happiness >= 0.1f) sb.Append(
                    string.Format("happiness {0:F1}%, ", emotionScores.Happiness * 100));
                if (emotionScores.Neutral >= 0.1f) sb.Append(
                    string.Format("neutral {0:F1}%, ", emotionScores.Neutral * 100));
                if (emotionScores.Sadness >= 0.1f) sb.Append(
                    string.Format("sadness {0:F1}%, ", emotionScores.Sadness * 100));
                if (emotionScores.Surprise >= 0.1f) sb.Append(
                    string.Format("surprise {0:F1}%, ", emotionScores.Surprise * 100));
                anger = "anger: " + emotionScores.Anger;
                contempt = "contempt: " + emotionScores.Contempt;
                disgust = "disgust: " + emotionScores.Disgust;
                fear = "fear: " + emotionScores.Fear;
                hapiness = "hapiness: " + emotionScores.Happiness;
                neutral = "neutral: " + emotionScores.Neutral;
                sadness = "sadness: " + emotionScores.Sadness;
                surprise = "surprise: " + emotionScores.Surprise;

                // Add glasses.
                sb.Append(face.FaceAttributes.Glasses);
                sb.Append(", ");
                glasses = face.FaceAttributes.Glasses.ToString();

                // Add hair.
                sb.Append("Hair: ");

                // Display baldness confidence if over 1%.
                if (face.FaceAttributes.Hair.Bald >= 0.01f)
                    sb.Append(string.Format("bald {0:F1}% ", face.FaceAttributes.Hair.Bald * 100));

                // Display all hair color attributes over 10%.
                IList<HairColor> hairColors = face.FaceAttributes.Hair.HairColor;
                StringBuilder hairBuilder = new StringBuilder();
                foreach (HairColor hairColor in hairColors)
                {
                    if (hairColor.Confidence >= 0.1f)
                    {
                        hairBuilder.Append(hairColor.Color.ToString());
                        hairBuilder.Append(string.Format(" {0:F1}% ", hairColor.Confidence * 100));
                        sb.Append(hairColor.Color.ToString());
                        sb.Append(string.Format(" {0:F1}% ", hairColor.Confidence * 100));
                    }
                }

                hair = hairBuilder.ToString();

                sb.Append("------------------------");

                if (accessories == "accessories: +System.Collections.Generic.List`1[Microsoft.Azure.CognitiveServices.Vision.Face.Models.Accessory]")
                {
                    accessories = "";
                }

                if (makeup == "makeup: Microsoft.Azure.CognitiveServices.Vision.Face.Models.Makeup")
                {
                    makeup = "";
                }
            }

            string[] facialRecogSave()
            {
                string[] facialRecogSaveKey = new string[15];
                facialRecogSaveKey[0] = age;
                facialRecogSaveKey[1] = gender;
                facialRecogSaveKey[2] = smile;
                facialRecogSaveKey[3] = accessories;
                facialRecogSaveKey[4] = anger;
                facialRecogSaveKey[5] = contempt;
                facialRecogSaveKey[6] = disgust;
                facialRecogSaveKey[7] = fear;
                facialRecogSaveKey[8] = sadness;
                facialRecogSaveKey[9] = surprise;
                facialRecogSaveKey[10] = neutral;
                facialRecogSaveKey[11] = hair;
                facialRecogSaveKey[12] = glasses;
                facialRecogSaveKey[13] = makeup;
                return facialRecogSaveKey;
            }

            if (!Directory.Exists("Saves"))
            {
                Directory.CreateDirectory("Saves");
            }
            saveEvent.saveConstructor("Saves", "facialSave", facialRecogSave());
            facialSaveNumber++;
            attributes = sb.ToString();

            return attributes;
        }

        public async void selectGUI()
        {
            /*
            // Get the image file to scan from the user.
            var openDlg = new OpenFileDialog();

            openDlg.Filter = "PNG Image(*.png)|*.png";
            bool? result = openDlg.ShowDialog(this);

            // Return if canceled.
            if (!(bool)result)
            {
                return;
            }*/
        }

        public void Select()
        {
            Console.WriteLine("Is the Image Local or Remote ---- Currently Only Accepts PNG Format");
            facialUser = Console.ReadLine().ToLower();
            //Remote Selection
            if (facialUser == "remote")
            {
                Console.WriteLine("Please Enter the Full URL of the File You Would Like to Scan");
                remoteImageUrl = Console.ReadLine();
                Execute();
            }

            //Local Selection
            else if (facialUser == "local")
            {
                Console.WriteLine("Please Enter The Full Path of the File You Would Like to Scan");
                localImagePath = Console.ReadLine();
                Execute();
            }
        }

        // Display the face attributes
        private static void DisplayAttributes(string attributes, string imageUri)
        {
            Console.WriteLine(imageUri);
            Console.WriteLine(attributes + "\n");
            exDisplay = attributes;
        }

        private static async Task DetectRemoteAsync(FaceClient faceClient, string imageUrl)
        {
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                Console.WriteLine("\nInvalid remoteImageUrl:\n{0} \n", imageUrl);
                return;
            }

            try
            {
                IList<DetectedFace> faceList =
                    await faceClient.Face.DetectWithUrlAsync(
                        imageUrl, true, false, faceAttributes);

                DisplayAttributes(GetFaceAttributes(faceList, imageUrl), imageUrl);
            }
            catch (APIErrorException e)
            {
                Console.WriteLine(imageUrl + ": " + e.Message);
            }
        }

        // + " " + " " + emotion + " " + facialHair + " " + glasses + " " + makeup + " " + hair + " " + smile
        // Detect faces in a local image
        private static async Task DetectLocalAsync(FaceClient faceClient, string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine(
                    "\nUnable to open or read localImagePath:\n{0} \n", imagePath);
                return;
            }

            try
            {
                using (Stream imageStream = File.OpenRead(imagePath))
                {
                    IList<DetectedFace> faceList =
                            await faceClient.Face.DetectWithStreamAsync(
                                imageStream, true, false, faceAttributes);
                    DisplayAttributes(GetFaceAttributes(faceList, imagePath), imagePath);
                }
            }
            catch (APIErrorException e)
            {
                Console.WriteLine(imagePath + ": " + e.Message);
            }
        }

        private static void Execute()
        {
            FaceClient faceClient = new FaceClient(
            new ApiKeyServiceClientCredentials(subscriptionKey),
            new System.Net.Http.DelegatingHandler[] { });
            faceClient.Endpoint = faceEndpoint;
            Console.WriteLine("Faces being detected ...");
            if (facialUser == "remote")
            {
                var t1 = DetectRemoteAsync(faceClient, remoteImageUrl);
            }
            else if (facialUser == "local")
            {
                var t2 = DetectLocalAsync(faceClient, localImagePath);
            }
        }
    }
}