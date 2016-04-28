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
				while (true)
				{
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
					else if (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.O)
					{
						Dialog dialog = new Dialog (bolt, "Are you sure you to overrite " + bolt.codeFile.FileName + "?");
						dialog.Chosen += (bool result) => {
							if (result) bolt.codeFile.Save();
						};
						bolt.AddComponent(dialog);
					}
					else if (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.X)
					{
						Dialog dialog = new Dialog (bolt, "Are you sure you to overrite " + bolt.codeFile.FileName + " and exit?");
						dialog.Chosen += (bool result) => {
							if (result) bolt.codeFile.Save();
						};
						bolt.AddComponent(dialog);
					}
					else
					{
						for (int i = 0; i < bolt.Components.Count; i++)
						{
							GraphicalInterface component = bolt.Components[i];
							if (component is InputListener) ((InputListener)component).KeyPressed(keyInfo);
						}
					}
				}
			});
		}

		public void StartListener()
		{
			listenerThread.Start ();
		}
	}
}

