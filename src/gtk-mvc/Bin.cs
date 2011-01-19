using System;
using System.Collections;
namespace Bizline.MVC
{
	public abstract class Bin:Gtk.Bin,IView
	{
		Hashtable _context;

		#region IView implementation
		public abstract ViewInputModel ParseInput ();

		public abstract void RenderOutput (ViewOutputModel output);

		public Hashtable Context {
			get {
				return this._context;
			}
			set{
				this._context = value;
			}
		}
		#endregion		
	}
}

