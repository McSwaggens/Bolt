using System;

namespace bolt
{
	public class Setting
	{
		public string name;
		object _value;
		
		public object @value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
				if (Bolt.instance != null && action != null)
					action();
			}
		}
		
		public Action action;
		
		public Setting (string name, object val)
		{
			this.name = name;
			@value = val;
			_value = val;
		}
		
		public Setting (string name, object val, Action action) : this (name, val)
		{
			this.action = action;
		}
	}
}

