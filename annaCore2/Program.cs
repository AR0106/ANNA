using ANNA.Interaction;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace ANNA
{
    internal class Program
    {
        internal static bool developerMode = true;

        internal static Guid guid()
        {
            return Guid.NewGuid();
        }

        private static string[] builtinCommands = { "hello", "world", "user"};

        protected internal static void RunANNA(string input)
        {
            // DEV DEBUG ONLY
#if DEBUG
            if (Output.directInput)
            {
                Console.WriteLine("Direct Input Mode");
            }
#endif

            // Built-In Commands
            if (builtinCommands.Any(input.Contains))
            {
                switch (input)
                {
                    case "hello":
                        Output.Speak("Hi! I'm Anna!");
                        return;

                    case "world":
                        Output.Speak("I'm on Earth! What world are you on?");
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
                        if (extInstance.SingleWordActions().Any(input.Contains))
                        {
                            // Runs Extension
                            extInstance.OnRun();
                        }
                    }
                }
            }
        }

        private static void Main(string[] args)
        {
            RunANNA(Output.ProcessInput(Console.ReadLine()));
        }
    }
}