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
		public Bolt bolt;

		public GraphicalInterface (Bolt bolt)
		{
			this.bolt = bolt;
		}

		public virtual void Update()
		{
		}

		public void Focus()
		{
			if (Focused)
				return;
			Focused = true;
			bolt.FocusedComponent = this;
			OnFocused ();
		}

		public void Unfocus()
		{
			if (!Focused)
				return;
			Focused = false;
			bolt.RemoveFocus (this);
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

		public void Dispose()
		{
			bolt.RemoveComponent (this);
			OnDisposed ();
			Unfocus ();
			bolt.Refresh ();
		}

		public virtual void OnDisposed()
		{
		}

		public void SelfUpdate() {
			bolt.CurrentlyUpdating = this;
			Update();
		}
	}
}

