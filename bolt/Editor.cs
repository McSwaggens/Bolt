using System;
using System.Collections.Generic;

namespace bolt
{
	public class Editor : GraphicalInterface, InputListener
	{
		public int TAB_SPACES = 8;
		public int Scroll = 0;
		public Location cursor = new Location(0, 0);
		public List<string> lines;
		public CodeFile codeFile;
		public int XWant = 0;

		public Editor (Bolt bolt, CodeFile codeFile) : base (bolt)
		{
			this.codeFile = codeFile;
			lines = new List<string>(codeFile.code.Split ('\n'));
			bolt.CurrentlyUpdating = this;

		}

		public override void Update ()
		{
			int i = Scroll;
			for (; i < lines.Count && i < size.Height; i++)
			{
				//TODO Syntax Highlighting
				GUI.ClearLine(i);
				GUI.DrawString((Settings.SHOW_LINE_NUMBERS ? (i + ":\t") : "") + lines[i], new Location(0, i));
			}
			for (; i < size.Height; i++)
				GUI.ClearLine(i);
			//ASSUMING TABS ARE 4 SPACES
			cursor.X += TAB_SPACES;
			GUI.SetCursorPos (cursor);
			cursor.X -= TAB_SPACES;

		}

		public void KeyPressed(ConsoleKeyInfo keyInfo)
		{
			if (!Focused)
			{
				return;
			}
			if (keyInfo.Key == ConsoleKey.UpArrow) {
				if (cursor.Y > 0) {
					cursor.Y--;
					if (cursor.X > lines [cursor.Y].Length) {
						cursor.X = lines [cursor.Y].Length;
					} else if (XWant > lines [cursor.Y].Length)
						cursor.X = lines [cursor.Y].Length;
					else
						cursor.X = XWant;
				}
			} else if (keyInfo.Key == ConsoleKey.DownArrow) {
				if (cursor.Y < lines.Count - 1) {
					cursor.Y++;
					if (cursor.X > lines [cursor.Y].Length) {
						cursor.X = lines [cursor.Y].Length;
					} else if (XWant > lines [cursor.Y].Length)
						cursor.X = lines [cursor.Y].Length;
					else
						cursor.X = XWant;
				}
			} else if (keyInfo.Key == ConsoleKey.LeftArrow) {
				if (cursor.X != 0) {
					cursor.X--;
					XWant = cursor.X;
				} else if (cursor.Y > 0) {
					cursor.Y--;
					cursor.X = lines [cursor.Y].Length;
				}
			} else if (keyInfo.Key == ConsoleKey.RightArrow) {
				if (cursor.X < lines [cursor.Y].Length) {
					cursor.X++;
					XWant = cursor.X;
				}
				else if (cursor.Y < lines.Count-1) {
					cursor.Y++;
					cursor.X = 0;
				}
			} else if (keyInfo.Key == ConsoleKey.Backspace) {
				if (cursor.X > 0 && cursor.X < lines [cursor.Y].Length + 1) {
					lines [cursor.Y] = lines [cursor.Y].Remove (cursor.X - 1, 1);
					cursor.X--;
					XWant = cursor.X;
					codeFile.Changed = true;
					SelfUpdate ();
					bolt.statusBar.SelfUpdate ();
				} else if (cursor.X == 0)
				{
					int width = lines [cursor.Y - 1].Length;
					lines [cursor.Y - 1] += lines [cursor.Y];
					lines.RemoveAt (cursor.Y);
					cursor.Y--;
					cursor.X = width;
					SelfUpdate ();
				}
			} else if (keyInfo.Key == ConsoleKey.Enter) {
				string[] split = new string[2];
				split [0] = lines [cursor.Y].Substring (0, cursor.X);
				split [1] = lines [cursor.Y].Substring (cursor.X);
				lines.Insert (cursor.Y, split[0]);
				cursor.Y++;
				lines [cursor.Y] = split [1];
				cursor.X = 0;
				codeFile.Changed = true;
				SelfUpdate ();
				bolt.statusBar.SelfUpdate ();
			}
			else {
				lines [cursor.Y] = lines [cursor.Y].Insert (cursor.X, "" + keyInfo.KeyChar);
				cursor.X++;
				codeFile.Changed = true;
				SelfUpdate ();
				bolt.statusBar.SelfUpdate ();
			}
			bolt.CurrentlyUpdating = this;
			//ASSUMING TABS ARE 4 SPACES
			cursor.X += TAB_SPACES;
			GUI.SetCursorPos (cursor);
			cursor.X -= TAB_SPACES;
		}

		public string GetCode() {
			string code = "";
			foreach (string line in lines) {
				code += line + "\n";
			}
			return code;
		}
	}
}

