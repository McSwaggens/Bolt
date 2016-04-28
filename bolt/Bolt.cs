using System;
using System.Collections.Generic;
using System.Threading;
namespace bolt
{
	public class Bolt
	{
		private string FileLocation;
		private List<GraphicalInterface> Components = new List<GraphicalInterface>();
		public GraphicalInterface CurrentlyUpdating = null;
		public InputManager inputManager;
		public bool Exiting = false;

		public Bolt (string fileLocation)
		{
			Console.Clear ();
			Console.CursorVisible = false;
			Console.TreatControlCAsInput = true;

			GUI.Initialize (this);

			StatusBar bar = new StatusBar (fileLocation);
			bar.Format(new Location(0, 0), new Size(GUI.ScreenWidth, 1));

			Components.Add (bar);
			
			FileLocation = fileLocation;

			inputManager = new InputManager (this);
			inputManager.StartListener ();

			//Draw components to screen
			Refresh ();

			//Infinite Loop
			while (!Exiting)
			{
			}
		}

		public void Refresh()
		{
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
	}
}

