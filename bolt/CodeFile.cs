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

		private bool changed = false;
		public bool Changed
		{
			get { return changed; }
			set
			{
				changed = value;
				Saved = false;
			}
		}

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
			try {
				File.WriteAllText(Location.Location + "/" + FileName, code);
				Changed = false;
				Saved = true;
			}
			catch (Exception e) {
				Saved = false;
			}
			finally {
				bolt.Bolt.TEMP_INSTANCE.statusBar.SelfUpdate ();
			}
		}
	}
}

