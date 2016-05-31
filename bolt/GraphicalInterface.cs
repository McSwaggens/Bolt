using System;

namespace bolt
{
	public class GraphicalInterface
	{
		public string name;
		public Location location;
		public Size size;
		public bool focused = false;
		public bool visible = true;
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
			if (focused)
				return;
			focused = true;
			bolt.FocusedComponent = this;
			OnFocused ();
		}

		public void Unfocus()
		{
			if (!focused)
				return;
			focused = false;
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

