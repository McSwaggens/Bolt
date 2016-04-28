using System;

namespace bolt
{
	public class GUI
	{
		private static Bolt bolt;

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
			location.X += bolt.CurrentlyUpdating.location.X;
			location.Y += bolt.CurrentlyUpdating.location.Y;

			Console.SetCursorPosition (location.X, location.Y);
			Console.Write (str);
		}

		public static void DrawString(string str, Location location, ConsoleColor ForeColor, ConsoleColor BackColor)
		{
			location.X += bolt.CurrentlyUpdating.location.X;
			location.Y += bolt.CurrentlyUpdating.location.Y;

			Console.ForegroundColor = ForeColor;
			Console.BackgroundColor = BackColor;

			Console.SetCursorPosition (location.X, location.Y);
			Console.Write (str);
		}

		public static void FillRectangle(Location start, Location end, ConsoleColor color)
		{
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

