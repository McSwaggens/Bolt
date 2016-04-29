using System;
using System.Collections.Generic;
using System.IO;
namespace bolt
{
	public class Settings
	{
		public static bool LOAD_FAILED = false;

		public static Dictionary<string, object> settings = new Dictionary<string, object> () {
			{ "SHOW_LINE_NUMBERS", true },
			{ "ENABLE_SYNTAX_HIGHLIGHTING", false },
			{ "ENABLE_SHORTCUT_BAR", true },
			{ "ENABLE_STATUS_BAR", true }
		};

		public static void LoadSettings (string configLocation) {
			try {
				Token[] tokens = ConfigLexer.GenerateTokens (File.ReadAllText (configLocation));
				settings = ConfigParser.Parse (tokens, settings);
			}catch (Exception e) {
				LOAD_FAILED = true;
			}
		}
	}
}

