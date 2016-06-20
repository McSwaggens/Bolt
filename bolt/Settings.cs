using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using bolt;

namespace bolt
{
	public class Settings
	{
		public static bool LOAD_FAILED = false;

		public List<Setting> settings = new List<Setting>()
		{
			new Setting("linenumbers", false)
		};
		
		public Setting this [string name]
		{
			get
			{
				foreach (Setting setting in settings)
				{
					if (setting.name == name)
						return setting;
				}
				throw new Exception($"No setting named \"{name}\" exists");
			}
			set
			{
				foreach (Setting setting in settings)
				{
					if (setting.name == name)
					{
						setting.value = value;
						return;
					}
				}
				throw new Exception($"No setting named \"{name}\" exists");
			}
		}
		
		public bool HasSetting(string name)
		{
			foreach (Setting setting in settings)
			{
				if (setting.name == name)
					return true;
			}
			return false;
		}

		public void LoadSettings (string configLocation) {
			if (!File.Exists(configLocation)) {
				string construct = "#Default Bolt config\n";
				foreach (Setting setting in settings)
				{
					construct += "set " + setting.name + " " + setting.value.ToString() + ";\n";
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

