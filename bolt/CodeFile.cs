using System;
using System.IO;
namespace bolt
{
	public class CodeFile
	{
		public SubDirectory Location;
		public string FileName;
		public string FileType;
		public bool Saved = false;
		public bool Changed = false;
		public string code;

		public CodeFile (SubDirectory directory, string fileName)
		{
			this.FileName = fileName;
			this.Location = directory;
			string[] split = fileName.Split ('.');
			this.FileType = split [split.Length-1];
			code = System.IO.File.ReadAllText (directory.Location + "/" + fileName);
		}

		public void Save(string code) {
			//StreamWriter sw = new StreamWriter(Location.Location + "/" + FileName);

			try {
				File.WriteAllText(Location.Location + "/" + FileName, code);
				Saved = true;
				Changed = false;
			}
			catch (Exception e) {
				Saved = false;
			}
			finally {
				
			}
		}
	}
}

