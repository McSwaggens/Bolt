using System;

namespace bolt
{
	public class CommandPanel : GraphicalInterface, InputListener
	{
		public CommandPanelMode mode = CommandPanelMode.INFO;
		
		public string currentCommand = "";
		
		public string currentNotification = "ERR!";
		
		public string defaultNotification
		{
			get
			{
				return $"\"{bolt.codeFile.FileName}\"";
			}
		}
		
		
		public CommandPanel (Bolt bolt) : base (bolt)
		{
			currentNotification = defaultNotification;
		}
		
		
		public void PushNotification(string notification)
		{
			currentNotification = notification;
			SelfUpdate();
			bolt.editor.ResetCursor();
		}

		public override void Update ()
		{
			if (mode == CommandPanelMode.INFO)
			{
				GUI.FillRectangle(new Location(0, 0), new Location(size.Width, 1), ConsoleColor.Black);
				GUI.DrawString(currentNotification, new Location(0, 0), ConsoleColor.Gray, ConsoleColor.Black);
			}
			else if (mode == CommandPanelMode.COMMAND)
			{
				GUI.FillRectangle(new Location(0, 0), new Location(size.Width, 1), ConsoleColor.Black);
				GUI.DrawString(":" + currentCommand, new Location(0, 0), ConsoleColor.Gray, ConsoleColor.Black);
				GUI.SetCursorPos(new Location(currentCommand.Length + 1, 0));
			}
		}
		
		public void KeyPressed(ConsoleKeyInfo keyInfo)
		{
			if (mode == CommandPanelMode.COMMAND && focused)
			{
				if (keyInfo.Key == ConsoleKey.Enter)
				{
					//Process command
					mode = CommandPanelMode.INFO;
					focused = false;
					bolt.SwitchFocus(bolt.editor);
					bolt.Refresh();
				}
				else if (keyInfo.Key == ConsoleKey.Backspace)
				{
					if (currentCommand.Length > 0) {
						currentCommand = currentCommand.Remove(currentCommand.Length-1, 1);
					}
					SelfUpdate();
				}
				else if (keyInfo.Key == ConsoleKey.Escape)
				{
					mode = CommandPanelMode.INFO;
					SelfUpdate();
					bolt.SwitchFocus(bolt.editor);
				}
				else
				{
					currentCommand += keyInfo.KeyChar;
					SelfUpdate();
				}
			}
			else
			{
				//Update line number
				//SelfUpdate();
			}
		}
		
		
		public override void OnFocused()
		{
			currentCommand = "";
			bolt.CurrentlyUpdating = this;
			Update();
		}
	}
	
	public enum CommandPanelMode
	{
		INFO, COMMAND, DIALOG
	}
}

