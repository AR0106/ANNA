﻿using ANNA.Interaction;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.IO;
using System.Reflection;

namespace ANNA
{
    internal class Program
    {
        internal static bool developerMode = true;

        private static string[] builtinCommands = { "hello", "world", "install"};

        private static void Main(string[] args)
        {
            // DEV DEBUG ONLY
            #if DEBUG
            if (Output.directInput)
            {
                Console.WriteLine("Direct Input Mode");
            }
            #endif

            string refInput = Output.ProcessInput(Console.ReadLine());

            // Built-In Commands
            if (builtinCommands.Any(refInput.Contains))
            {
                switch (refInput)
                {
                    case "hello":
                        Output.Speak("Hi! I'm Anna!");
                        return;

                    case "world":
                        Output.Speak("I'm on Earth! What world are you on?");
                        return;

                    case "install":
                        Extensions.InstallExtension();
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