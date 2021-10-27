using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NullLib.CommandLine;

namespace Null.LibClassViewer
{
    class Program
    {
        private static void PrefixPr(string prefix, object obj)
        {
            foreach (string line in $"{obj}".Split('\n'))
                Console.WriteLine($"{prefix} {line}");
        }
        private static void Pr(object obj) => PrefixPr(".", obj);
        private static void Log(object obj) => PrefixPr(":", obj);
        private static void Warn(object obj) => PrefixPr("!", obj);
        private static void What(object obj) => PrefixPr("?", obj);
        private static void Error(object obj) => PrefixPr("@", obj);
        private static void EndCmd()
        {
            Console.WriteLine();
        }

        public class ViewerCommands : CommandHome
        {
            List<Type> allTypes = new List<Type>();


            [Command(typeof(ArguConverter))]
            public void LoadAssembly(string assembly)
            {
                try
                {
                    Assembly newAssmebly = Assembly.LoadFrom(assembly);
                    Type[] types = newAssmebly.GetTypes();
                    allTypes.AddRange(types);

                    Pr($"Assembly laded, type count: {types.Length}");
                }
                catch (Exception e)
                {
                    Log($"Assembly load failed, {e.Message}:");
                    Error(e.StackTrace);
                }
            }

            [Command]
            public void ShowTypes()
            {
                foreach (Type t in allTypes)
                    Pr(t);
            }

            [Command(typeof(ArguConverter), typeof(BoolArguConverter))]
            public void ShowTypes(string typeName, bool fullName = false)
            {
                IEnumerable<Type> toSearch;
                if (fullName)
                    toSearch = allTypes.Where(v => v.FullName == typeName);
                else
                    toSearch = allTypes.Where(v => v.Name == typeName);

                foreach (Type t in toSearch)
                    Pr(t);
            }

            [Command(typeof(ArguConverter), typeof(BoolArguConverter))]
            public void ShowSubTypes(string typeName, bool fullName = false)
            {
                IEnumerable<Type> toSearch;
                if (fullName)
                    toSearch = allTypes.Where(v => v.FullName == typeName);
                else
                    toSearch = allTypes.Where(v => v.Name == typeName);

                foreach (Type _toSearch in toSearch)
                    foreach (Type t in allTypes.Where(t => _toSearch.IsAssignableFrom(t)))
                        Pr(t);
            }

            [Command]
            public string Help()
            {
                return CommandObject.GenCommandOverviewText();
            }

            [Command]
            public string Help(string cmdname)
            {
                return CommandObject.GenCommandDetailsText(cmdname, StringComparison.OrdinalIgnoreCase);
            }
        }
        static CommandObject<ViewerCommands> ViewerCmdObj { get; } = new CommandObject<ViewerCommands>();

        static void Main(string[] args)
        {
            Initialize();

            while (true)
            {
                Console.Write(">>> ");
                if (ViewerCmdObj.TryExecuteCommand(Console.ReadLine(), true, out object rst))
                {
                    if (rst is object)
                        Pr(rst);
                }
                else
                {
                    Error("Execute failed");
                }

                EndCmd();
            }
        }

        private static void Initialize()
        {
            Console.WriteLine("Null.LibClassViewer v0.1 (dev)\n");
        }
    }
}
