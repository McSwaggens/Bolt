using System;
using System.Collections.Generic;

namespace bolt
{
	public class DefCommands
	{
		private static List<Command> definedCommands = new List<Command>()
		{
			new Command("print", (args) => {
				if (Bolt.instance != null)
				{
					string combine = "";
					foreach (Token token in args)
					{
						if (token is Word)
						{
							string name = (string)token.raw;
							if (Bolt.instance.settings.HasSetting(name))
								combine += Bolt.instance.settings[name].value + " ";
							else
							{
								Notification.Push($"Cannot find variable {name}", NotificationType.ERROR);
								return;
							}
						}
						else
						{
							combine += token.raw + " ";
						}
					}
					Notification.Push(combine);
				}
			}),
			new Command("set", (args) => {
				if (args [0] is Word && Bolt.instance.settings.HasSetting ((string)((Word)args [0]).raw)) {
					string setting = (string)args [0].raw;
					object val = Bolt.instance.settings [setting].value;
						
					if (val is string) {
						if (args [1] is bolt.String) {
							Bolt.instance.settings [setting].value = ((String)args [1]).raw;
						} else {
							Notification.Push ($"Expected type string for setting {(string)args[0].raw}", NotificationType.ERROR);
						}
					} else if (val is bool) {
						if (args [1] is bolt.Boolean) {
							Bolt.instance.settings [setting].value = ((Boolean)args [1]).raw;
						} else {
							Notification.Push ($"Expected type boolean for setting {(string)args[0].raw}", NotificationType.ERROR);
						}
					} else if (val is int) {
						if (args [1] is bolt.Integer) {
							Bolt.instance.settings [setting].value = ((Integer)args [1]).raw;
						} else {
							Notification.Push ($"Expected type integer for setting {(string)args[0].raw}", NotificationType.ERROR);
						}
					}
				}
			}),
			new Command("quit", (args) => {
				Bolt.instance.Quit();
			}),
			new Command("q", (args) => {
				Bolt.instance.Quit();
			}),
			new Command("write", (args) => {
				Bolt.instance.Save();
				Bolt.instance.commandPanel.PushNotification($"Saved \"{Bolt.instance.codeFile.FileName}\"", ConsoleColor.Gray);
				
			}),
			new Command("w", (args) => {
				Bolt.instance.Save();
				Bolt.instance.commandPanel.PushNotification($"Saved \"{Bolt.instance.codeFile.FileName}\"", ConsoleColor.Gray);
			}),
			new Command("wq", (args) => {
				Bolt.instance.Save();
				Bolt.instance.Quit();
			})
		};
		
		public static bool Contains (string name)
		{
			foreach (Command command in definedCommands)
			{
				if (command.name == name)
				{
					return true;
				}
			}
			return false;
		}
		
		public static Command Get (string name)
		{
			foreach (Command command in definedCommands)
			{
				if (command.name == name)
				{
					return command;
				}
			}
			return null;
		}
	}
}

