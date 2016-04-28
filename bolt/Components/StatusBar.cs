using System;

namespace bolt
{
	public class StatusBar : GraphicalInterface
	{
		public string FileLocation;

		public StatusBar (string fileLocation)
		{
			FileLocation = fileLocation;
		}

		public override void Update ()
		{
			GUI.FillRectangle(new Location(0, 0), new Location(GUI.ScreenWidth, 1), ConsoleColor.White);
			GUI.DrawString ("  Bolt v1.0 Alpha", new Location(0, 0), ConsoleColor.Black, ConsoleColor.White);
			GUI.DrawString (FileLocation, new Location(GUI.ScreenWidth / 3, 0), ConsoleColor.Black, ConsoleColor.White);
		}

		public override void OnUnfocused ()
		{
		}

		public override void OnFocused ()
		{
		}
	}
}

