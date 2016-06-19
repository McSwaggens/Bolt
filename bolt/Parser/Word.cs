using System;

namespace bolt
{
	public class Word : Token
	{
		public Word (string raw)
		{
			this.raw = raw;
		}

		public Token Clasify()
		{
			//Generate new token
			return null;
		}

		public override string ToString ()
		{
			return "" + raw;
		}
	}
}

