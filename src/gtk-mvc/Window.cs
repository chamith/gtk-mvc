using System;
using System.Collections;
using Gtk;
namespace Bizline.MVC
{
	public abstract class Window : Gtk.Window,IView
	{
		public Window (WindowType windowType) : base(windowType)
		{

		}

		Hashtable _context;

		#region IView implementation
		public abstract ViewInputModel ParseInput ();

		public abstract void RenderOutput (ViewOutputModel output);

		public Hashtable Context {
			get { return this._context; }
			 set { this._context = value; }
		}
		
		#endregion
		

	}
}

