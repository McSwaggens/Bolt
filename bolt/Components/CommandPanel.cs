using System;

namespace bolt
{
	public class CommandPanel : GraphicalInterface, InputListener
	{
		public string currentCommand = "";
		
		public CommandPanel (Bolt bolt) : base (bolt)
		{
		}

		public override void Update ()
		{
			//GUI.FillRectangle (new Location (0, 0), new Location (size.Width, 1), ConsoleColor.Black);
			
			if (!this.focused)
			{
				GUI.FillRectangle(new Location(0, 0), new Location(size.Width, 1), ConsoleColor.Black);
				GUI.DrawString($"\"{bolt.codeFile.FileName}\"", new Location(0, 0), ConsoleColor.Gray, ConsoleColor.Black);
			}
			else
			{
				GUI.FillRectangle(new Location(0, 0), new Location(size.Width, 1), ConsoleColor.Black);
				GUI.DrawString(":" + currentCommand, new Location(0, 0), ConsoleColor.Gray, ConsoleColor.Black);
			}
		}
		
		public void KeyPressed(ConsoleKeyInfo keyInfo)
		{
			if (focused)
			{
				if (keyInfo.Key == ConsoleKey.Enter)
				{
					//Process command
					focused = false;
					bolt.SwitchFocus(bolt.editor);
					bolt.Refresh();
				}
				else if (keyInfo.Key == ConsoleKey.Backspace)
				{
					currentCommand = currentCommand.Remove(currentCommand.Length-1, 1);
				}
				else
				{
					currentCommand += keyInfo.KeyChar;
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
}

