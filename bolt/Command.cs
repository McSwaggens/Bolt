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
	}
}

