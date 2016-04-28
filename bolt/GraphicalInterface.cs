using System;

namespace bolt
{
	public class GraphicalInterface
	{
		public string Name;
		public Location location;
		public Size size;
		public bool Focused = false;
		public bool Shown = true;
		public virtual void Update()
		{
		}

		public void Focus()
		{
			Focused = true;
			OnFocused ();
		}

		public void Unfocus()
		{
			Focused = false;
			OnUnfocused ();
		}

		public virtual void OnUnfocused()
		{
		}

		public virtual void OnFocused()
		{
		}

		public void Format(Location location, Size size) {
			this.size = size;
			OnResized ();
			this.location = location;
			OnMoved ();
		}

		public void Move(Location location) {
			this.location = location;
			OnMoved();
		}

		public void Resize(Size size) {
			this.size = size;
			OnResized ();
		}

		public virtual void OnResized()
		{
		}

		public virtual void OnMoved()
		{
		}
	}
}

