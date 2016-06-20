namespace bolt
{
	public class Setting
	{
		public string name;
		public object @value;
		
		public Setting (string name, object val)
		{
			this.name = name;
			@value = val;
		}
	}
}

