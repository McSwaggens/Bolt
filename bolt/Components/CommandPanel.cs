using System;
using System.Collections.Generic;

namespace bolt
{
	public class CommandPanel : GraphicalInterface, InputListener
	{
		public CommandPanelMode mode = CommandPanelMode.INFO;
		
		public string currentCommand = "";
		
		public string currentNotification = "ERR!";
		public ConsoleColor notificationForeColor = ConsoleColor.Gray;
		
		List<char> symbols = new List<char>("+-!@#$%^&*():;,.?/~`\\|=<>{}[]".ToCharArray());
		
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
		
		
		public void PushNotification(string notification, ConsoleColor foreColor)
		{
			notificationForeColor = foreColor;
			currentNotification = notification;
			SelfUpdate();
			bolt.editor.ResetCursor();
		}

		public override void Update ()
		{
			if (mode == CommandPanelMode.INFO)
			{
				GUI.FillRectangle(new Location(0, 0), new Location(size.Width, 1), ConsoleColor.Black);
				GUI.DrawString(currentNotification, new Location(0, 0), notificationForeColor, ConsoleColor.Black);
			}
			else if (mode == CommandPanelMode.COMMAND)
			{
				GUI.FillRectangle(new Location(0, 0), new Location(size.Width, 1), ConsoleColor.Black);
				GUI.DrawString(":" + currentCommand, new Location(0, 0), ConsoleColor.Gray, ConsoleColor.Black);
				GUI.SetCursorPos(new Location(currentCommand.Length+1, 0));
			}
		}
		
		public void KeyPressed(ConsoleKeyInfo keyInfo)
		{
			if (mode == CommandPanelMode.COMMAND && focused)
			{
				if (keyInfo.Key == ConsoleKey.Enter)
				{
					//Parse command
					Parser.Parse(currentCommand + ";\n", bolt.settings);
					
					
					//Switch focus from current panel to editor
					PushDefaultNotification();
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
					PushDefaultNotification();
					mode = CommandPanelMode.INFO;
					SelfUpdate();
					bolt.SwitchFocus(bolt.editor);
				}
				else if (char.IsLetterOrDigit (keyInfo.KeyChar) || symbols.Contains(keyInfo.KeyChar) || keyInfo.KeyChar == ' ')
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
		
		public void PushDefaultNotification()
		{
			currentNotification = defaultNotification;
			notificationForeColor = ConsoleColor.Gray;
		}
		
		
		public override void OnFocused()
		{
			currentCommand = "";
			SelfUpdate();
			GUI.SetCursorPos(new Location(1, 0));
		}
	}
	
	public enum CommandPanelMode
	{
		INFO, COMMAND, DIALOG
	}
}

