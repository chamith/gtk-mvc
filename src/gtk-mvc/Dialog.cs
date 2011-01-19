using System;
using System.Collections;
using Gtk;
namespace Bizline.MVC
{
	public abstract class Dialog:Gtk.Dialog, IView
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
		
		public Dialog()
		{
			this.DeleteEvent += delegate(object o, DeleteEventArgs args) {
				//args.RetVal = true;
			};
		}
	}
}

