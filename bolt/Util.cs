using System;

namespace bolt
{
	public class Util
	{
		public static string StringSpacer(int space)
		{
			string spacer = "";
			for (int i = 0; i < space; i++) 
				spacer += " ";
			return spacer;
		}
	}
}

