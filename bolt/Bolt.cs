using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using static bolt.Clas;
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
		public StatusBar statusBar;
		public ControlsPanel controlsPanel;
		public static Bolt TEMP_INSTANCE;

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

		public Bolt (string[] args)
		{
			string fileLocation = args[0];
			if (!fileLocation.Contains("/")) {
				fileLocation = Directory.GetCurrentDirectory() + "/" + fileLocation;
			}
			//Load parameters from command line arguments
			LoadParams(args);
			
			if (!DO_EXECUTE) return;
			
			if (!File.Exists(fileLocation))
			{
				File.Create(fileLocation).Close();
			}
			
			//Uncomment for file input information, aswell as; working directory.
			///Console.WriteLine("DIR: " + Directory.GetCurrentDirectory());
			///Console.WriteLine(fileLocation);
			///return;
			
			//TODO: check for NO_LOAD_CONFIG flag in command line args
			Settings.LoadSettings (((OSInfo.OS_OSX || OSInfo.OS_WINDOWS) ? "/Users" : "/home") + "/daniel/.bolt");
			if (Settings.LOAD_FAILED) {
				Logger.LogError ("Failed to load settings... Exiting.");
				return;
			}

			TEMP_INSTANCE = this;

			Console.Clear ();

			GUI.Initialize (this);

			statusBar = new StatusBar (this, fileLocation);
			statusBar.Format(new Location(0, 0), new Size(GUI.ScreenWidth, 1));

			Components.Add (statusBar);

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

			//Console Initialization
			//Undone when CTRL+C is pressed in InputManager.cs
			Console.CursorVisible = true;
			Console.TreatControlCAsInput = true;

			//Initialize Text Ballet (Bottom of screen)
			controlsPanel = new ControlsPanel(this);
			controlsPanel.Format (new Location (0, GUI.ScreenHeight), new Size (GUI.ScreenWidth, 1));
			AddComponent (controlsPanel);

			//Initialize Text/Code Editor
			editor = new Editor(this, codeFile);
			editor.Format (new Location (0, 1), new Size (GUI.ScreenWidth, GUI.ScreenHeight - 2));
			editor.Focus ();
			AddComponent (editor);

			//Draw components to screen
			Refresh ();

			inputManager.StartListener ();
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
			editor.codeFile.Save (editor.GetCode ());
		}
	}
}

