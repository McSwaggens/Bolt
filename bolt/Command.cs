using System;

namespace bolt
{
	public class Command
	{
		public string name;
		public Action<Token[]> action;

		public Command (string name, Action<Token[]> action)
		{
			this.action = action;
			this.name = name;
		}
		
		public static void Execute(string command)
		{
			Token[] tokens = Lexer.GenerateTokens(command + "\n");
			Parser.Parse(tokens, Bolt.instance.settings);
		}
	}
}

