using System;
using System.Collections.Generic;

namespace bolt
{
	public class Editor : GraphicalInterface, InputListener
	{
		public static int TAB_SPACES => ((bool)Bolt.instance.settings["linenumbers"].value) ? 4 : 0;
		public int Scroll = 0;
		public Location cursor = new Location(0, 0);
		public List<string> lines;
		public CodeFile codeFile;
		public int XWant = 0;
		
		List<char> symbols = new List<char>("+-!@#$%^&*():;,.?/~`\\|=<>{}[]".ToCharArray());

		public Editor (Bolt bolt, CodeFile codeFile) : base (bolt)
		{
			this.codeFile = codeFile;
			lines = new List<string>(codeFile.code.Split ('\n'));
			bolt.CurrentlyUpdating = this;

		}

		public override void Update ()
		{
			int i = 0;
			for (; i + Scroll < lines.Count && i < size.Height; i++)
			{
				//TODO Syntax Highlighting
				GUI.ClearLine(i);
				bool showLineNumbers = (bool)bolt.settings ["linenumbers"].value;
				
				if (showLineNumbers)
				{
					GUI.DrawString((i + Scroll) + "", new Location(0, i));
					GUI.DrawString(lines[i + Scroll], new Location(TAB_SPACES, i));
				}
				else
				{
					GUI.DrawString(lines[i], new Location(0, i));
				}
				
				
				//GUI.DrawString((showLineNumbers ? ((i + Scroll) + ":\t") : "") + lines[i + Scroll], new Location(0, i));
			}
			for (; i < size.Height; i++)
				GUI.ClearLine(i);
			cursor.X += TAB_SPACES;
			GUI.SetCursorPos (cursor);
			cursor.X -= TAB_SPACES;
		}
		
		public override void OnFocused()
		{
			SelfUpdate();
		}
		
		public void ResetCursor()
		{
			UpdateLine(cursor.Y + Scroll);
		}
		
		public void UpdateLine (int line) {
			bolt.CurrentlyUpdating = this;
			GUI.ClearLine(line + Scroll);
			bool showLineNumbers = (bool)bolt.settings ["linenumbers"].value;
			
			if (showLineNumbers)
			{
				GUI.DrawString((line) + "", new Location(0, line + Scroll));
				GUI.DrawString(lines[line], new Location(TAB_SPACES, line + Scroll));
			}
			else
			{
				GUI.DrawString(lines[line], new Location(0, line + Scroll));
			}
			
			cursor.X += TAB_SPACES;
			GUI.SetCursorPos (cursor);
			cursor.X -= TAB_SPACES;
		}

		public void KeyPressed(ConsoleKeyInfo keyInfo)
		{
			if (!focused)
			{
				return;
			}
			if (keyInfo.Key == ConsoleKey.UpArrow) {
				if (cursor.Y == 0) {
					if (Scroll != 0) {
						Scroll--;
						if (XWant > lines [cursor.Y + Scroll].Length)
							cursor.X = lines [cursor.Y + Scroll].Length;
						else
							cursor.X = XWant;
						SelfUpdate ();
					}
				} else {
					cursor.Y--;
					if (XWant > lines [cursor.Y + Scroll].Length)
						cursor.X = lines [cursor.Y + Scroll].Length;
					else
						cursor.X = XWant;
				}
			} else if (keyInfo.Key == ConsoleKey.DownArrow) {
				if (cursor.Y == size.Height-1) {
					if (cursor.Y + Scroll != lines.Count-1) {
						Scroll++;
						if (XWant > lines [cursor.Y + Scroll].Length)
							cursor.X = lines [cursor.Y + Scroll].Length;
						else
							cursor.X = XWant;
						SelfUpdate ();
					}
				} else if (cursor.Y + Scroll != lines.Count-1) {
					cursor.Y++;
					if (XWant > lines [cursor.Y + Scroll].Length)
						cursor.X = lines [cursor.Y + Scroll].Length;
					else
						cursor.X = XWant;
				}
			} else if (keyInfo.Key == ConsoleKey.LeftArrow) {
				if (cursor.X == 0) {// Check if the cursor is at the bottom of the line
					if (Scroll != 0 && cursor.Y == 0) {
						Scroll--;
						cursor.Y--;
						cursor.X = lines [cursor.Y + Scroll].Length;
						XWant = cursor.X;
					} else if (cursor.Y != 0) {
						cursor.Y--;
						cursor.X = lines [cursor.Y + Scroll].Length;
						XWant = cursor.X;
					}
				} else { // Cursor is not at the bottom of the line
					cursor.X--;
					XWant = cursor.X; // Set the XWant
				}
			} else if (keyInfo.Key == ConsoleKey.RightArrow) {
				if (cursor.X == lines [cursor.Y + Scroll].Length) {
					if (Scroll + cursor.Y < lines.Count-1 && cursor.Y == size.Height) {
						Scroll++;
						cursor.Y++;
						cursor.X = 0;
						XWant = 0;
					} else if (cursor.Y + Scroll < lines.Count-1) {
						cursor.Y++;
						cursor.X = 0;
						XWant = 0;
					}
				} else {
					cursor.X++;
					XWant = cursor.X;
				}
			} else if (keyInfo.Key == ConsoleKey.Backspace) {
				if (cursor.X > 0 && cursor.X < lines [cursor.Y + Scroll].Length + 1) {
					lines [cursor.Y + Scroll] = lines [cursor.Y + Scroll].Remove (cursor.X - 1, 1);
					cursor.X--;
					XWant = cursor.X;
					codeFile.Changed = true;
					UpdateLine(cursor.Y + Scroll);
				} else if (cursor.X == 0 && cursor.Y != 0)
				{
					int width = lines [(cursor.Y + Scroll)- 1].Length;
					lines [(cursor.Y + Scroll) - 1] += lines [cursor.Y + Scroll];
					lines.RemoveAt (cursor.Y + Scroll);
					cursor.Y--;
					cursor.X = width;
					XWant = width;
					SelfUpdate ();
				}
			} else if (keyInfo.Key == ConsoleKey.Enter) {
				string[] split = new string[2];
				split [0] = lines [cursor.Y + Scroll].Substring (0, cursor.X);
				split [1] = lines [cursor.Y + Scroll].Substring (cursor.X);
				lines.Insert (cursor.Y + Scroll, split[0]);
				cursor.Y++;
				lines [cursor.Y + Scroll] = split [1];
				cursor.X = 0;
				codeFile.Changed = true;
				SelfUpdate ();
			}
			else {
				char key = keyInfo.KeyChar;
				if (char.IsSymbol(key) || char.IsLetterOrDigit(key) || symbols.Contains(key) || key == ' ')
				{
					lines [cursor.Y + Scroll] = lines [cursor.Y + Scroll].Insert (cursor.X, "" + key);
					cursor.X++;
					codeFile.Changed = true;
				}
				
				UpdateLine(cursor.Y + Scroll);
			}
			bolt.CurrentlyUpdating = this;
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

