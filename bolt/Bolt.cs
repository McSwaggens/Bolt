using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
namespace bolt
{
	public class Bolt
	{
		public List<GraphicalInterface> Components = new List<GraphicalInterface>();
		public GraphicalInterface CurrentlyUpdating = null;
		public InputManager inputManager;
		public bool Exiting = false;
		public Editor editor;
		public GraphicalInterface FocusedComponent;

		public SubDirectory RootDirectory;
		//Code file only for now
		public CodeFile codeFile;
		public bool isRepository;

		/*
		 * Controls;
		 *  Exit: CTRL+C
		 * 	Save: CTRL+O (O for out)
		 * 	Exit and Save (CTRL+X)
		 */

		public Bolt (string fileLocation)
		{

			Console.Clear ();

			GUI.Initialize (this);

			StatusBar bar = new StatusBar (this, fileLocation);
			bar.Format(new Location(0, 0), new Size(GUI.ScreenWidth, 1));

			Components.Add (bar);

			isRepository = Directory.Exists (fileLocation) && File.Exists(fileLocation);

			if (isRepository) {
				//Editing a directory (Repository)
				RootDirectory = new SubDirectory (fileLocation);
				RootDirectory.ScanDirectory ();
			} else {
				//Editing a single file
				codeFile = new CodeFile (new SubDirectory (fileLocation), Path.GetFileName(fileLocation));
			}

			inputManager = new InputManager (this);
			inputManager.StartListener ();

			//Console Initialization
			//Undone when CTRL+C is pressed in InputManager.cs
			Console.CursorVisible = true;
			Console.TreatControlCAsInput = true;

			//Initialize Text/Code Editor
			editor = new Editor(this, codeFile);
			editor.Format (new Location (0, 1), new Size (GUI.ScreenWidth, GUI.ScreenHeight - 1));
			editor.Focus ();
			AddComponent (editor);

			//Draw components to screen
			Refresh ();

			//Infinite Loop
			while (!Exiting)
			{
			}
		}

		public void Refresh()
		{
			Console.ResetColor ();
			Console.Clear ();
			foreach (GraphicalInterface component in Components)
			{
				CurrentlyUpdating = component;
				component.Update();
			}
			CurrentlyUpdating = null;
		}

		public void RefreshComponent(GraphicalInterface component)
		{
			CurrentlyUpdating = component;
			component.Update ();
			CurrentlyUpdating = null;
		}

		public void AddComponent(GraphicalInterface component)
		{
			this.Components.Add (component);
			//Redraw the screen after the new component is added
			Refresh ();
		}

		public void RemoveComponent(GraphicalInterface component)
		{
			this.Components.Remove (component);
			//Redraw the screen after the component is removed
			Refresh ();
		}

		public void RemoveFocus(GraphicalInterface component)
		{
			FocusedComponent = Components [Components.Count-1];
			FocusedComponent.Focus ();
		}

		public void Save()
		{
			
		}
	}
}

