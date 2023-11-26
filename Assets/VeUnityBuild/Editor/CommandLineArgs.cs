using System;
using System.Collections.Generic;

namespace VeUnityBuild.Editor
{
    public class CommandLineArgs
    {
        private readonly Dictionary<string, string> _dict = new();

        public CommandLineArgs()
        {
            var args = Environment.GetCommandLineArgs();
            for (var i = 0; i < args.Length; i++)
            {
                if (!args[i].StartsWith('-')) continue;

                if (i + 1 < args.Length && !args[i + 1].StartsWith('-'))
                {
                    _dict.Add(args[i], args[i + 1]);
                }
                else
                {
                    _dict.Add(args[i], string.Empty);
                }
            }
        }

        public bool Has(string key)
        {
            return _dict.ContainsKey(key);
        }

        public string GetValue(string key)
        {
            return _dict[key];
        }
    }
}