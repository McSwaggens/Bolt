﻿using System;

namespace bolt
{
	public class Integer : ValueTokenType
	{
		public Integer (string raw)
		{
			this.raw = long.Parse (raw);
		}

		public override string ToString ()
		{
			return "" + raw;
		}
	}
}

