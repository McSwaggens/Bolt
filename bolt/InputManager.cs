using System;
using System.Threading;
namespace bolt
{
	public class InputManager
	{
		private Bolt bolt;
		private Thread listenerThread;
		public InputManager (Bolt bolt)
		{
			this.bolt = bolt;

			listenerThread = new Thread (() => {
				ConsoleKeyInfo keyInfo = Console.ReadKey();
				if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 && keyInfo.Key == ConsoleKey.C)
				{
					Console.ResetColor();
					Console.Clear();
					Console.CursorVisible = true;
					bolt.Exiting = true;
					Thread.Sleep(10);
					Console.WriteLine("CTRL+C Pressed... Exiting.");
					Environment.Exit(0);
				}
				else if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 && keyInfo.Key == ConsoleKey.S)
				{
					
				}
			});
		}

		public void StartListener()
		{
			listenerThread.Start ();
		}
	}
}

