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

