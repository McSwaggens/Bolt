using System;
using System.Threading;
namespace bolt
{
	public delegate void InputEvent(ConsoleKeyInfo keyInfo);
	
	public class InputManager
	{
		public static event InputEvent OnKeyPressed;
		
		private Bolt bolt;
		private Thread listenerThread;
		public bool CancelKeySpread = false;
		public InputManager (Bolt bolt)
		{
			this.bolt = bolt;
		}
		
		public void StartListener()
		{
			while (true) // TODO: Exit listener
			{
				ConsoleKeyInfo keyInfo = Console.ReadKey();
				
				if (OnKeyPressed != null)
					OnKeyPressed(keyInfo);
				
				if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 && keyInfo.Key == ConsoleKey.C)
				{
					Console.ResetColor();
					Console.Clear();
					Console.CursorVisible = true;
					bolt.Exiting = true;
					GUI.StopGUIEventListener ();
					Thread.Sleep(10);
					Console.WriteLine("CTRL+C Pressed... Exiting.");
					Environment.Exit(0);
				}
				else if (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.O)
				{
					Dialog dialog = new Dialog (bolt, "Are you sure you to overrite " + bolt.codeFile.FileName + "?");
					dialog.Chosen += (bool result) => {
						if (result) bolt.Save();
					};
					bolt.AddComponent(dialog);
				}
				else if (keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == ConsoleKey.X)
				{
					Dialog dialog = new Dialog (bolt, "Are you sure you to overrite " + bolt.codeFile.FileName + " and exit?");
					dialog.Chosen += (bool result) => {
						if (result) bolt.Save();
						bolt.Exiting = true;
						Console.ResetColor();
						Console.Clear();
						Console.CursorVisible = true;
						Environment.Exit(0);
					};
					bolt.AddComponent(dialog);
				}
				else
				if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 && keyInfo.Key == ConsoleKey.E)
				{
					bolt.commandPanel.mode = CommandPanelMode.COMMAND;
					bolt.SwitchFocus(bolt.commandPanel);
				}
				else
				{
					if (bolt.FocusedComponent != null && bolt.FocusedComponent is InputListener)
						((InputListener)bolt.FocusedComponent).KeyPressed (keyInfo);
				}
			}
		}
	}
}

