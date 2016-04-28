using System;

namespace bolt
{
	public class Logger
	{
		public static void LogColor(string text, ConsoleColor color)
		{
			ConsoleColor prevColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			Console.ForegroundColor = prevColor;
		}

		public static void LogError(string text)
		{
			ConsoleColor prevColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(text);
			Console.ForegroundColor = prevColor;
		}
	}
}

