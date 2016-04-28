using System;

namespace bolt
{
	public class Editor : GraphicalInterface, InputListener
	{
		public int TAB_SPACES = 8;
		public int Scroll = 0;
		public Location cursor = new Location(0, 0);
		public string[] lines;
		public CodeFile codeFile;

		public Editor (Bolt bolt, CodeFile codeFile) : base (bolt)
		{
			this.codeFile = codeFile;
			lines = codeFile.code.Split ('\n');
		}

		public override void Update ()
		{
			for (int i = Scroll; i < lines.Length && i < size.Height; i++)
			{
				//TODO Syntax Highlighting
				GUI.ClearLine(i);
				GUI.DrawString((Settings.SHOW_LINE_NUMBERS ? (i + ":\t") : "") + lines[i], new Location(0, i));
			}
			//ASSUMING TABS ARE 4 SPACES
			cursor.X += TAB_SPACES;
			GUI.SetCursorPos (cursor);
			cursor.X -= TAB_SPACES;
		}

		public void KeyPressed(ConsoleKeyInfo keyInfo)
		{
			if (keyInfo.Key == ConsoleKey.UpArrow) {
				if (cursor.Y != 0)
					cursor.Y--;
			} else if (keyInfo.Key == ConsoleKey.DownArrow) {
				cursor.Y++;
			} else if (keyInfo.Key == ConsoleKey.LeftArrow) {
				if (cursor.X != 0)
					cursor.X--;
			} else if (keyInfo.Key == ConsoleKey.RightArrow) {
				cursor.X++;
			} else if (keyInfo.Key == ConsoleKey.Backspace) {
				if (cursor.X > 0 && cursor.X < lines [cursor.Y].Length) {
					lines [cursor.Y] = lines [cursor.Y].Remove (cursor.X - 1, 1);
					cursor.X--;
				}
			}
			else {
				lines [cursor.Y] = lines [cursor.Y].Insert (cursor.X, "" + keyInfo.KeyChar);
				cursor.X++;
			}
			SelfUpdate ();
		}
	}
}

