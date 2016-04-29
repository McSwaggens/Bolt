using System;

namespace bolt
{
	public class GUI
	{
		private static Bolt bolt;
		private static readonly bool ENABLED = true; //TURN TO FALSE FOR LOGGER.cs OUTPUT

		public static void Initialize(Bolt bolt)
		{
			GUI.bolt = bolt;
		}

		public static int ScreenWidth
		{
			get
			{
				return Console.BufferWidth;
			}
		}

		public static int ScreenHeight
		{
			get
			{
				return Console.BufferHeight-1;
			}
		}

		public static void DrawString(string str, Location location)
		{
			if (!ENABLED)
				return;
			location.X += bolt.CurrentlyUpdating.location.X;
			location.Y += bolt.CurrentlyUpdating.location.Y;

			Console.SetCursorPosition (location.X, location.Y);
			Console.Write (str);
		}

		public static void ClearLine(int line)
		{
			line += bolt.CurrentlyUpdating.location.Y;
			string wiper = Util.StringSpacer (bolt.CurrentlyUpdating.size.Width);
			DrawString(wiper, new Location(0, line-1));
		}

		public static void DrawString(string str, Location location, ConsoleColor ForeColor, ConsoleColor BackColor)
		{
			location.X += bolt.CurrentlyUpdating.location.X;
			location.Y += bolt.CurrentlyUpdating.location.Y;

			Console.ForegroundColor = ForeColor;
			Console.BackgroundColor = BackColor;

			Console.SetCursorPosition (location.X, location.Y);
			Console.Write (str);
			Console.ResetColor();
		}

		public static void SetCursorPos(Location loc)
		{
			//LAZY CODE
			//TODO FIX LAZY CODE

			Location location = new Location (loc.X, loc.Y);

			location.X += bolt.CurrentlyUpdating.location.X;
			location.Y += bolt.CurrentlyUpdating.location.Y;
			Console.SetCursorPosition (location.X, location.Y);
		}

		public static void FillRectangle(Location start, Location end, ConsoleColor color)
		{
			if (!ENABLED)
				return;
			start.X += bolt.CurrentlyUpdating.location.X;
			start.Y += bolt.CurrentlyUpdating.location.Y;

			end.X += bolt.CurrentlyUpdating.location.X;
			end.Y += bolt.CurrentlyUpdating.location.Y;

			Console.BackgroundColor = color;

			int x = start.X;
			string Spacer = Util.StringSpacer(end.X);

			for (int y = start.Y; y < end.Y; y++)
			{
				Console.SetCursorPosition (x, y);
				Console.Write (Spacer);
			}

			Console.ResetColor ();
		}
	}
}

