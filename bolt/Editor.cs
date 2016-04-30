using System;
using System.Collections.Generic;

namespace bolt
{
	public class Editor : GraphicalInterface, InputListener
	{
		//public static int TAB_SPACES => ((bool)Settings.settings["SHOW_LINE_NUMBERS"]) ? 8 : 0;
		public static int TAB_SPACES = 8;
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
			int i = 0;
			for (; i < lines.Count && i < size.Height; i++)
			{
				//TODO Syntax Highlighting
				GUI.ClearLine(i);
				bool showLineNumbers = (bool)Settings.settings ["SHOW_LINE_NUMBERS"];
				GUI.DrawString((showLineNumbers ? ((i + Scroll) + ":\t") : "") + lines[i + Scroll], new Location(0, i));
			}
			for (; i < size.Height; i++)
				GUI.ClearLine(i);
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
				cursor.Y--;
				if (cursor.Y + Scroll >= 0) {
					if (cursor.X > lines [cursor.Y + Scroll].Length) {
						cursor.X = lines [cursor.Y + Scroll].Length;
						if (cursor.Y == -1) {
							cursor.Y++;
							Scroll--;
						}
					} else if (cursor.Y == -1 && Scroll > 0) {
						Scroll--;
						cursor.Y++;
						SelfUpdate ();
					}
					if (XWant > lines [cursor.Y + Scroll].Length)
						cursor.X = lines [cursor.Y + Scroll].Length;
					else
						cursor.X = XWant;
				} else
					cursor.Y = 0;
			} else if (keyInfo.Key == ConsoleKey.DownArrow) {
				if (cursor.Y + Scroll < lines.Count - 1) {
					cursor.Y++;
					if (cursor.X > lines [cursor.Y + Scroll].Length) {
						cursor.X = lines [cursor.Y + Scroll].Length;
					} else if (cursor.Y > size.Height - 1) {
						Scroll++;
						cursor.Y--;
						SelfUpdate ();
					}
					if (XWant > lines [cursor.Y + Scroll].Length)
						cursor.X = lines [cursor.Y + Scroll].Length;
					else
						cursor.X = XWant;
				}
			} else if (keyInfo.Key == ConsoleKey.LeftArrow) {
				if (cursor.X != 0) {
					cursor.X--;
					XWant = cursor.X;
				} else if (cursor.Y > 0) {
					cursor.Y--;
					cursor.X = lines [cursor.Y + Scroll].Length;
				}
			} else if (keyInfo.Key == ConsoleKey.RightArrow) {
				if (cursor.X < lines [cursor.Y + Scroll].Length) {
					cursor.X++;
					XWant = cursor.X;
				}
				else if (cursor.Y + Scroll < lines.Count-1) {
					cursor.Y++;
					Scroll++;
					cursor.X = 0;
				}
			} else if (keyInfo.Key == ConsoleKey.Backspace) {
				if (cursor.X > 0 && cursor.X < lines [cursor.Y].Length + 1) {
					lines [cursor.Y + Scroll] = lines [cursor.Y + Scroll].Remove (cursor.X - 1, 1);
					cursor.X--;
					XWant = cursor.X;
					codeFile.Changed = true;
					SelfUpdate ();
					bolt.statusBar.SelfUpdate ();
				} else if (cursor.X == 0 && cursor.Y != 0)
				{
					int width = lines [cursor.Y - 1].Length;
					lines [(cursor.Y + Scroll) - 1] += lines [cursor.Y + Scroll];
					lines.RemoveAt (cursor.Y);
					cursor.Y--;
					cursor.X = width;
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
				bolt.statusBar.SelfUpdate ();
			}
			else {
				lines [cursor.Y + Scroll] = lines [cursor.Y + Scroll].Insert (cursor.X, "" + keyInfo.KeyChar);
				cursor.X++;
				codeFile.Changed = true;
				SelfUpdate ();
				bolt.statusBar.SelfUpdate ();
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

