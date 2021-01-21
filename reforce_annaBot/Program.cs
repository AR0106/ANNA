using annaLibCore;
using Reforce_annaBotML.Model;
using reforceLibCore;
using System;
using System.IO;
using System.Linq;
using System.Text;
using WikipediaNet;
using WikipediaNet.Objects;

namespace reforce_annaBot
{
    internal class Program
    {
        //Strings
        public static string userInput = inputType().Prediction;

        //Static

        //Arrays
        //String Arrays
        //Keywords

        public static ModelOutput inputType()
        {
            ModelInput input = new ModelInput
            {
                PHRASE = Console.ReadLine()
            };

            return ConsumeModel.Predict(input);
        }

        private static moduleParser moduleParser = new moduleParser();

        public static void Main()
        {
            try
            {
                //Bot Code
                userInput = inputType().Prediction;

                // Displays Date and Time
                if (userInput == "time")
                {
                    Console.WriteLine(DateTime.Now.ToString());
                    Console.WriteLine("");
                    userInput = inputType().Prediction;
                    Main();
                }

                //If Anything Isn't Entered
                else if (userInput == "")
                {
                    Console.WriteLine("Seems Like You Didn't Enter Anything. Please Try Again!");
                    Main();
                }

                //If The User Wants To Exit
                else if (userInput == "exit")
                {
                    Console.WriteLine("Are You Sure You Want To Exit?");
                    userInput = inputType().Prediction;
                    if (userInput == "yes")
                    {
                    }
                    else if (userInput == "no")
                    {
                        Console.Clear();
                        Main();
                    }
                    else
                    {
                        Console.WriteLine("Please Enter Yes or No");
                    }
                }

                //Calls The Facial Recognition File
                else if (userInput == "facial.scan")
                {
                    fRecognition fRecognition = new fRecognition();
                    fRecognition.Select();
                    userInput = inputType().Prediction;
                    Main();
                }

                //Debug Command to Force Save
                //--------------------------------
                //        PENDING REWRITE
                //--------------------------------
                else if (userInput == "file.save")
                {
                }

                //Calls The EXIF Removal Method From the ANNA Library
                else if (userInput == "exif.remove")
                {
                    Console.Write("Please Enter The Image's Path: ");
                    userInput = inputType().Prediction;
                    string imagePath = userInput;
                    Console.WriteLine("Please Enter The Image's Name and Extension: ");
                    userInput = inputType().Prediction;
                    string imageName = userInput;
                    Console.WriteLine("Do You Want an ID Prefix?");
                    userInput = inputType().Prediction;

                    // Handler for if the User Doesn't Wish to Add a File Prefix
                    if (userInput == "no")
                    {
                        exifRemoval.RemoveAndSave(imagePath, imageName);
                        if (exifRemoval.isComplete == true)
                        {
                            Console.WriteLine("EXIF Removal Complete: " + imagePath);
                            Console.ReadLine();
                            Console.Clear();
                            Main();
                        }
                        else
                        {
                            Console.WriteLine("Something Went Wrong, Please Try Again");
                            Console.ReadLine();
                            Console.Clear();
                            Main();
                        }
                    }

                    // Handler For If The User Wishes To Add a File Prefix
                    else if (userInput == "yes")
                    {
                        Console.WriteLine("Please Enter Your Prefered Prefix: ");
                        string oPrefix = Console.ReadLine().ToString();
                        exifRemoval.RemoveAndSave(imagePath, imageName, oPrefix);
                        if (exifRemoval.isComplete == true)
                        {
                            Console.WriteLine("EXIF Removal Complete: " + imagePath);
                            Console.ReadLine();
                            Console.Clear();
                            Main();
                        }
                        else
                        {
                            Console.WriteLine("Something Went Wrong, Please Try Again");
                            Console.ReadLine();
                            Console.Clear();
                            Main();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Main();
                    }
                }

                // Searches Wikipedia
                /*else
                {
                    Wikipedia wikipedia = new Wikipedia();
                    wikipedia.Limit = 1;

                    QueryResult result = wikipedia.Search(userInput);

                    foreach (Search search in result.Search)
                    {
                        if (limitCheck < wikipedia.Limit)
                        {
                            Console.WriteLine("Found " + result.Search.Count);
                            string titleHtml = search.Title;
                            string snippetHtml = search.Snippet;
                            Console.WriteLine(htmlSanitize.getText(titleHtml));
                            if (search.Score != null)
                            {
                                Console.WriteLine("Score: " + search.Score);
                            }
                            Console.WriteLine(htmlSanitize.getText(snippetHtml) + "...");
                            Console.WriteLine(search.Url);
                            Console.WriteLine("---------------------------------------------------");
                            Console.WriteLine(limitCheck);
                            limitCheck = limitCheck + 1;
                        }
                    }

                    userInput = inputType().Prediction;
                    Main();
                }*/
                else
                {
                    Console.WriteLine("Black");
                    moduleParser.initModules();

                    foreach (var module in moduleParser.methodList)
                    {
                        if (module.callPhrases.Any(userInput.Contains))
                        {
                            module.parameters[0] = userInput;
                            Console.WriteLine("Found");
                            moduleParser.CallModules(module.typeName, module.method, module.parameters);
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("None Found");
                        }
                    }
                }
            }

            //Saves and Constructs Logs
            catch (Exception e)
            {
            }
        }
    }
}