using System;
using System.IO;
namespace bolt
{
	class MainClass
	{
		
		public static void Main (string[] args)
		{
			if (args.Length > 0) {
				new Bolt (args);
			} else
				Logger.LogError ("Bolt requires arguments to start.");
		}
	}
}
