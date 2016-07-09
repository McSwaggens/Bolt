using System;
using System.Collections.Generic;

namespace bolt
{
	public class DefCommands
	{
		public static List<Command> definedCommands = new List<Command>()
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
							combine += Bolt.instance.settings[name].value + " ";
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
	}
}

