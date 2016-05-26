using System;

namespace bolt
{
	public class ControlsPanel : GraphicalInterface
	{
		private static string[] controls = new string[] {
			"^O Save",
			"^X Save and exit",
			"^C Exit without saving",
			"^K Delete line"
		};


		public ControlsPanel (Bolt bolt) : base (bolt)
		{
			
		}

		public override void Update ()
		{
			int division = GetMaxPosible();
			int pos = 0;
			GUI.FillRectangle (new Location (0, 0), new Location (size.Width, 1), ConsoleColor.White);
			for (int i = 0; i < division; i++)
			{
				string control = controls [i];
				if (i != division - 1)
					control += " | ";
				GUI.DrawString (control, new Location(pos, 0), ConsoleColor.Black, ConsoleColor.White);
				pos+= control.Length;
			}
		}

		int GetMaxPosible() {
			int len = 0;
			int pos = 0;
			foreach (string control in controls) {
				len += control.Length + 3;
				if (len < size.Width)
					pos++;
			}
			return pos;
		}
	}
}

