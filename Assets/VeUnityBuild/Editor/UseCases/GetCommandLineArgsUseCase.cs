using System;
using System.Collections.Generic;

namespace VeUnityBuild.Editor.UseCases
{
    public class GetCommandLineArgsUseCase
    {
        readonly Dictionary<string, string> dict = new();

        public GetCommandLineArgsUseCase()
        {
            var args = Environment.GetCommandLineArgs();
            for (var i = 0; i < args.Length; i++)
            {
                if (!args[i].StartsWith('-'))
                {
                    continue;
                }

                if (i + 1 < args.Length && !args[i + 1].StartsWith('-'))
                {
                    dict.Add(args[i], args[i + 1]);
                }
                else
                {
                    dict.Add(args[i], string.Empty);
                }
            }
        }

        public bool Has(string key)
        {
            return dict.ContainsKey(key);
        }

        public string GetValue(string key)
        {
            dict.TryGetValue(key, out var value);
            return value;
        }
    }
}
