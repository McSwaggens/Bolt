using System;
using System.Collections.Generic;

namespace bolt {
    public class Clas {
        
        public static Dictionary<string, object> Setters  = new Dictionary<string, object>() {
            {"MISSING", "MISSING"}
        };
        
        private static Dictionary<string, bool> Flags = new Dictionary<string, bool>()
        {
            { "dc",   false }, // Default config
            { "", true }
        };
        
        public static bool DO_EXECUTE = true;
        
        private static Dictionary<List<string>, string> TextArguments = new Dictionary<List<string>, string>() {
            {new List<string>{"h", "help"}, 
            @"
--------------------[Bolt Help]--------------------

[Usage]

bolt [File] [[- | --] argument] ...

[Description]
Bolt is an open source modular terminal text editor with syntax highlighting functionality
 written in C# for GNU/Linux or OSX systems.


[Command Line Arguments]
Type these in for the desired effect.

-dc (default config)
    Ignores the ~/.bolt config.

    
[Github]
http://www.Github.com/McSwaggens/Bolt/

--------------------[PashRuntime Help]--------------------
"},
            {new List<string>{"v", "version"}, 
@"
Bolt v1.0
"}
        };
        
        public static void LoadParams(string[] args) {
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].StartsWith("-"))
                    {
                        
                        ///TODO: make difference between - and --
                        string flag = args[i].StartsWith("--")
                            ? args[i].TrimStart("--".ToCharArray())
                            : args[i].TrimStart("-".ToCharArray());
                        
                        if (string.IsNullOrWhiteSpace(flag))
                            continue;
                        
                        string TextOut;
                        if (ContainsTextArgKey(flag, out TextOut)) {
                            Console.WriteLine(TextOut);
                            DO_EXECUTE = false;
                            return;
                        }
                        if (ContainsSetterArgKey(flag)) {
                            i++;
                            int outint;
                            if (int.TryParse(args[i], out outint))
                                Setters[flag] = outint;
                            else Setters[flag] = flag;
                        }
                        else
                        if (Flags.ContainsKey(flag))
                            Flags[flag] = !Flags[flag];
                        else {
                            Logger.LogError("Unknown flag: " + flag);
                            Logger.LogWarning("Aborting...");
                            return;
                        }
                    }
                }
            }
        }
        
        private static bool ContainsTextArgKey(string key, out string value) {
            value = "";
            foreach (KeyValuePair<List<string>, string> pair in TextArguments) {
                foreach (string tKey in pair.Key) if (tKey == key) { value = pair.Value; return true; }
            }
            return false;
        }
        
        private static bool ContainsSetterArgKey(string key) {
            foreach (KeyValuePair<string, object> pair in Setters) {
                if (pair.Key == key) return true;
            }
            return false;
        }
    }
}