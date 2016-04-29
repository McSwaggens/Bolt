using System;

namespace bolt
{
	public class Character : ValueTokenType
	{
		public Character (string c)
		{
			raw = c [0];
		}

		public override string ToString ()
		{
			return "" + raw;
		}
	}
}

