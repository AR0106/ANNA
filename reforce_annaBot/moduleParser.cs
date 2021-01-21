using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace reforce_annaBot
{
    internal class moduleParser
    {
        public static XDocument moduleXML = XDocument.Load("modules.xml");
        public static moduleInfo[] methodList;
        public static int index = 0;
        private static int currentIndex = 0;

        public struct moduleInfo
        {
            public string method;
            public object[] parameters;
            public string[] callPhrases;
            public string typeName;
        };

        // Calls the Main Module Method
        public static void CallModules(string typeName, string methodName, object[] parameters = null)
        {
            Type type = Type.GetType(typeName);
            var flags = BindingFlags.Static | BindingFlags.Public;
            var method = type.GetMethod(methodName, flags);
            method.Invoke(method, parameters);
        }

        public static void initModules()
        {
            // Registers Number of Modules
            foreach (var module in moduleXML.Root.Elements("module"))
            {
                index++;
            }

            methodList = new moduleInfo[index];

            foreach (var module in moduleXML.Root.Elements("module"))
            {
                // Gets the Main Method of the Module
                methodList[currentIndex].method = module.Element("method").Value.ToString();

                // Gets the Parameters of the Main Method
                methodList[currentIndex].parameters = module.Element("parameters")
                    .Elements("param")
                    .Select(p => (object)(string)p.Element("value"))
                    .ToArray();

                // Gets the Phrases Users need to Say in Order to Call the Function
                methodList[currentIndex].callPhrases = module.Element("callPhrases")
                    .Elements("phrase")
                    .Select(p => (string)p.Element("value"))
                    .ToArray();

                methodList[currentIndex].typeName = module.Element("typeName").Value.ToString();

                currentIndex++;
            }
        }
    }
}