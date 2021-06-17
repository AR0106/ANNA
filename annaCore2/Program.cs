using ANNA.Interaction;
using ANNA.UserInfo;
using ANNA.BasicCommands;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ANNA
{
    internal class Program
    {
        internal static bool developerMode = true;

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

        private static string[] builtinCommands = { "hello", "from", "user", "ANEID", "search", "time" };

        protected internal static void RunANNA(string input, string[] args)
        {
            // DEV DEBUG ONLY
#if DEBUG
            if (Output.directInput)
            {
                Output.Responses.Add(new Response("Direct Input Mode"));
            }
#endif

            // Built-In Commands
            if (builtinCommands.Any(input.Contains))
            {
                switch (input)
                {
                    case "hello":
                        Output.Responses.Add(new Response(Greeting.GetGreeting(new User("Testmark").FirstName)));
                        return;

                    case "world":
                        Output.Responses.Add(new Response("I was developed by Reforce Labs in Wisconsin, but I have servers throughout the world."));
                        return;

                    case "ANEID":
#if DEBUG
                        baseANEID();
#endif
                        return;

                    case "search":
                        BasicCommands.WebSearch.RunSearch(args[0]);
                        return;

                    case "time":
                        Output.Responses.Add(new Response($"It's {DateTime.Now.Hour}:{DateTime.Now.Minute}"));
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
                            Response response = new Response(extInstance.OnRun(args));
                            Output.Responses.Add(response);
                        }
                    }
                }
            }
        }
    }
}