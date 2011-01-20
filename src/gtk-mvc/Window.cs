using System;
using System.Collections;
using Gtk;
namespace Gtk.Mvc
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
		
		
		public void InvokeWithCallback(string action, IView callbackView, string callbackMethodName, params object[] args)
		{
			FrontController.InvokeWithCallback(action, this, callbackView, callbackMethodName, args);
		}
		
		public void Invoke(string action, params object[] args)
		{
			FrontController.Invoke(action, this, args);
		}
}
}

