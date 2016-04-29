using System;
using System.IO;
namespace bolt
{
	class MainClass
	{
		
		public static void Main (string[] args)
		{
			//TODO: Implement CLAS
			if (args.Length > 0) {
				new Bolt (args);
			} else
				Logger.LogError ("Please enter a file to be executed");
		}
	}
}
