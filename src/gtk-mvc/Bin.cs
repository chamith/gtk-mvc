using System;
using System.Collections;
namespace Gtk.Mvc
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
	
		public void InvokeWithCallback(string action, IView callbackView, string callbackMethodName, params object[] args)
		{
			FrontController.InvokeWithCallback(action, this, callbackView, callbackMethodName, args);
		}
		
		public void Invoke(string action, params object[] args)
		{
			FrontController.Invoke(action, this, args);
		}
	}
	
	public class CallbackInfo
	{
		public IView View {
			get;
			set;
		}
		public string MethodName {
			get;
			set;
		}
	}
	
}

