using System;

namespace bolt
{
	public class Editor : GraphicalInterface, InputListener
	{
		public int Scroll = 0;
		public string[] lines;
		public CodeFile codeFile;

		public Editor (Bolt bolt, CodeFile codeFile) : base (bolt)
		{
			this.codeFile = codeFile;
		}

		public override void Update ()
		{
			for (int i = Scroll; i < lines && i < size.Height; i++)
			{
				//TODO Syntax Highlighting
				GUI.DrawString(i + ":\t" + lines[i]);
			}
		}

		public void KeyPressed(ConsoleKeyInfo keyInfo)
		{
		}
	}
}

