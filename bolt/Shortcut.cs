using System;

namespace bolt
{
	public class Shortcut
	{
		public ConsoleKeyInfo keyInfo;
		
		public Action action;
		
		public Shortcut (ConsoleKeyInfo keyInfo)
		{
			this.keyInfo = keyInfo;
		}
		
		public Shortcut (Action action, ConsoleKey key, bool ctrl = false, bool alt = false, bool shift = false)
		{
			this.action = action;
			keyInfo = new ConsoleKeyInfo('\0', key, shift, alt, ctrl);
		}
		
		public bool Match(ConsoleKeyInfo info)
			=> keyInfo.Modifiers == info.Modifiers && keyInfo.Key == info.Key;
			
		public void Fire()
		{
			action();
		}
	}
}

