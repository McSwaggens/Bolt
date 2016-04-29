using System;

namespace bolt
{
	public class Dialog : GraphicalInterface, InputListener
	{
		public delegate void DialogAnswered(bool result);
		public event DialogAnswered Chosen;
		private string Title = "Missing Title!";
		private const int Seperation = 3;
		private ConsoleColor backColor = ConsoleColor.White;
		private ConsoleColor foreColor = ConsoleColor.Black;
		public Dialog (Bolt bolt, string Title,
			ConsoleColor foreColor = ConsoleColor.White,
			ConsoleColor backColor = ConsoleColor.Black) : base (bolt)
		{
			this.Title = Title;
			this.backColor = backColor;
			this.foreColor = foreColor;

			Format (new Location (0, GUI.ScreenHeight / 3), new Size (GUI.ScreenWidth, GUI.ScreenHeight / 3));

			Focus ();
		}

		public override void Update ()
		{
			//Draw the background
			GUI.FillRectangle (new Location (0, 0), new Location(size.Width, size.Height), ConsoleColor.White);

			//Draw the title
			GUI.DrawString(Title, new Location((size.Width / 2) - (Title.Length / 2), 2), ConsoleColor.Black, ConsoleColor.White);

			GUI.DrawString(" Y - Yes ", new Location((size.Width / 2) - (9) - Seperation, size.Height / 2), ConsoleColor.White, ConsoleColor.Black);//9

			GUI.DrawString(" N - No ", new Location((size.Width / 2) + Seperation, size.Height / 2), ConsoleColor.White, ConsoleColor.Black);//8
		}


		/*
		 * Check for;
		 * Y
		 * N
		 * UP
		 * DOWN
		 * LEFT
		 * RIGHT
		 * ENTER
		 */
		public void KeyPressed(ConsoleKeyInfo keyInfo)
		{
			if (keyInfo.Key == ConsoleKey.Y) {
				Chosen (true);
				Dispose ();
			} else if (keyInfo.Key == ConsoleKey.N) {
				Chosen (false);
				Dispose ();
			} else
				SelfUpdate ();
			bolt.inputManager.CancelKeySpread = true;
		}
	}
}

