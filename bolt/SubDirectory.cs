using System;
using System.Collections.Generic;
using System.IO;
namespace bolt
{
	public class SubDirectory
	{
		public string Name;
		public string Location;
		public List<CodeFile> files = new List<CodeFile>();
		public List<SubDirectory> directories = new List<SubDirectory>();

		public SubDirectory (string Location, string Name)
		{
			this.Name = Name;
			this.Location = Location;
		}

		public SubDirectory (string Location)
		{
			this.Location = Path.GetDirectoryName (Location);
			//Lazy code
			//TODO: Replace this code with correct version
			Name = this.Location.Replace(Path.GetDirectoryName (this.Location) + "/", "");
		}

		public void ScanDirectory()
		{
			foreach (string file in Directory.GetFiles(Location)) {
				files.Add (new CodeFile (this, file));
				Logger.Log ($"{Location} added CodeFile {file}");
			}
			foreach (string directory in Directory.GetDirectories(Location)) {
				directories.Add (new SubDirectory (directory));
			}
		}
	}
}

