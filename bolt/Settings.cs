using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace bolt
{
	public class Settings
	{
		public static bool LOAD_FAILED = false;

		public Dictionary<string, object> settings = new Dictionary<string, object> () {
			{ "linenumbers", false },
			{ "syntax_highlighting", false }
		};

		public void LoadSettings (string configLocation) {
			if (!File.Exists(configLocation)) {
				string construct = "#Default Bolt config\n";
				foreach (KeyValuePair<string, object> pair in settings) {
					construct += "set " + pair.Key + " " + pair.Value.ToString() + ";\n";
				}
				File.Create(configLocation).Close();
				Thread.Sleep(10);
				File.WriteAllText(configLocation, construct);
			}
			else {
				try {
					Token[] tokens = Lexer.GenerateTokens (File.ReadAllText (configLocation));
					 Parser.Parse (tokens, this);
				}catch (Exception e) {
					LOAD_FAILED = true;
				}
			}
		}
	}
}

