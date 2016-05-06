using System;

namespace bolt
{
	public class StatusBar : GraphicalInterface
	{
		public string FileLocation;

		public StatusBar (Bolt bolt, string fileLocation) : base (bolt)
		{
			FileLocation = fileLocation;

		}

		public override void Update ()
		{
			GUI.FillRectangle(new Location(0, 0), new Location(size.Width, 1), ConsoleColor.White);
			GUI.DrawString ("  Bolt v1.0 Alpha", new Location(0, 0), ConsoleColor.Black, ConsoleColor.White);
			GUI.DrawString (FileLocation, new Location((size.Width / 2) - (FileLocation.Length / 2), 0), ConsoleColor.Black, ConsoleColor.White);
			string strEndDraw = "No Changes";
			if (bolt.codeFile.Saved)
				strEndDraw = "Saved";
			else if (bolt.codeFile.Changed)
				strEndDraw = "Modified";
			GUI.DrawString (strEndDraw, new Location (size.Width - strEndDraw.Length - 1, 0), ConsoleColor.Black, ConsoleColor.White);
		}

		public override void OnUnfocused ()
		{
		}

		public override void OnFocused ()
		{
		}
	}
}

