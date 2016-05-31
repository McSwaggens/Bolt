using System;

namespace bolt
{
	public delegate void LoggerEvent(string message);
	
	public class Logger
	{
		public static event LoggerEvent OnLoggedAll;
		
		public static event LoggerEvent OnNormalLog;
		public static event LoggerEvent OnErrorLogged;
		public static event LoggerEvent OnWarningLogged;
		
		public static void LogColor(string text, ConsoleColor color)
		{
			ConsoleColor prevColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			Console.ForegroundColor = prevColor;
			
			if (OnLoggedAll != null)
				OnLoggedAll(text);
			if (OnNormalLog != null)
				OnNormalLog(text);
		}

		public static void LogError(string text)
		{
			ConsoleColor prevColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(text);
			Console.ForegroundColor = prevColor;
			
			if (OnLoggedAll != null)
				OnLoggedAll(text);
			if (OnErrorLogged != null)
				OnErrorLogged(text);
		}
		
		public static void LogWarning(string text)
		{
			ConsoleColor prevColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(text);
			Console.ForegroundColor = prevColor;
			
			if (OnLoggedAll != null)
				OnLoggedAll(text);
			if (OnWarningLogged != null)
				OnWarningLogged(text);
		}

		public static void Log(string text)
		{
			Console.WriteLine(text);
			
			if (OnLoggedAll != null)
				OnLoggedAll(text);
			if (OnNormalLog != null)
				OnNormalLog(text);
		}
	}
}

