using ANNA.Interaction;
using ANNA.UserInfo;
using ANNA.BasicCommands;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace ANNA
{
    internal class Program
    {
#if DEBUG
        internal static bool developerMode = true;
#endif

        public static DateTime GetLinkerTimestampUtc(Assembly assembly)
        {
            var location = assembly.Location;
            return GetLinkerTimestampUtc(location);
        }

        public static DateTime GetLinkerTimestampUtc(string filePath)
        {
            const int peHeaderOffset = 60;
            const int linkerTimestampOffset = 8;
            var bytes = new byte[2048];

            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                file.Read(bytes, 0, bytes.Length);
            }

            var headerPos = BitConverter.ToInt32(bytes, peHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(bytes, headerPos + linkerTimestampOffset);
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return dt.AddSeconds(secondsSince1970);
        }

        internal static string baseANEID()
        {
            return $"00_reforcelabs_anna_{GetLinkerTimestampUtc(Assembly.GetExecutingAssembly()).Ticks / 12}";
        }

        internal static string[] builtinCommands = { "hello", "from", "user", "ANEID", "search", "time", "meaning" };

        protected internal static void RunANNA(string input, string[] args)
        {
            // DEV DEBUG ONLY
#if DEBUG
            if (Output.directInput)
                Output.PushResponse(new Response("Direct Input Mode"));
#endif

            Output.PushResponse(new Response("ANNA Initialized"));

            // Built-In Commands
            if (builtinCommands.Any(input.Contains))
            {
                switch (input)
                {
                    case "hello":
                        Output.PushResponse(new Response(Greeting.GetGreeting(new User(args[0]).FirstName)));
                        return;

                    case "world":
                        Output.PushResponse(new Response("I was developed by Reforce Labs in Wisconsin, but I have servers throughout the world."));
                        return;

                    case "ANEID":
                        if (developerMode)
                        {
                            Output.PushResponse(new Response(baseANEID()));
                        }
                        return;

                    case "search":
                        WebSearch.RunSearch(args[0]);
                        return;

                    case "time":
                        Output.PushResponse(new Response($"It's {DateTime.Now.Hour}:{DateTime.Now.Minute}"));
                        return;

                    case "meaning":
                        Output.PushResponse(new Response($"The definition of {args[0]} is {Dictionary.GetDefinition(args[0], args[1])}"));
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
                    Assembly assembly = Assembly.LoadFrom(extension);

                    IEnumerable<Type> extensionTypes = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(s => s.GetTypes())
                        .Where(p => typeof(Extension).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

                    // Instatiates Each Extension
                    foreach (var extensionType in extensionTypes)
                    {
                        var extInstance = (IAnnaCommand)Activator.CreateInstance(extensionType);

                        // Checks if it is the Correct Command
                        if (extInstance.SingleWordActions().Any(input.Contains))
                        {
                            // Runs Extension
                            Response response = new Response(extInstance.OnRun(args));
                            Output.PushResponse(response);
                        }
                    }
                }
            }
        }
    }
}