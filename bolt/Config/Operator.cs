using System;

namespace bolt
{
	public class Operator : Token
	{
		public OperatorType type;
		public Operator (OperatorType type)
		{
			this.type = type;
		}

		public override string ToString ()
		{
			return "" + type.ToString();
		}
	}
}

