using System;

namespace bolt
{
	public class Notification
	{
		private static ConsoleColor[] notificationColors =
		{
			ConsoleColor.Gray,
			ConsoleColor.Yellow,
			ConsoleColor.Red
		};
		
		private static char[] notificationChars =
		{
			'#',
			'?',
			'!'
		};
		
		public static void Push (string message)
		{
			if (Bolt.instance == null)
				return;
			
			Bolt.instance.commandPanel.PushNotification( '~' + message, ConsoleColor.Gray);
		}
		
		public static void Push (string message, ConsoleColor color)
		{
			if (Bolt.instance == null)
				return;
			
			Bolt.instance.commandPanel.PushNotification( '~' + message, color);
		}
		
		public static void Push (string message, NotificationType type)
		{
			if (Bolt.instance == null)
				return;
			
			Bolt.instance.commandPanel.PushNotification( '~' + notificationChars[(int)type] + message, ConsoleColor.Gray);
		}
	}
}

