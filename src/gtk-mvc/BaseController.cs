
using System;
using Gtk;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Bizline.MVC.Configuration;
namespace Bizline.MVC
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class BaseController:IController
	{
		public string Area {
			get;
			set;
		}
		public string Controller {
			get;
			set;
		}
		public string CurrentAction {
			get;
			set;
		}
		
		//public Hashtable Context{get;internal set;}
		
		public ActionRequest Request {get;internal set;}

		public ViewOutputModel Output {
			get;
			set;
		}
		public IView Referrer
		{
			get
			{
				return this.Request.View;
			}
		}
		
		public void DestroyReferrer()
		{
			if(this.Referrer!=null)
				(this.Referrer as Widget).Destroy();
		}
		
		public void RenderView(string view)
		{
			FrontController.RenderView(this.Area, this.Controller, view, this.Output);
		}
		
		public void RenderView(IView view, string method, params object[] args)
		{
			FrontController.RenderView(view, method, args);
		}
		
		public void Rerender(IView view)
		{
			FrontController.Rerender(view, this.Output);
		}
		
		public void RenderWidget(string view, Gtk.Container container)
		{
			FrontController.RenderWidget(this.Area, this.Controller, view, container, this.Output);
		}
		
		public void ShowDialog(string view)
		{
			FrontController.RunDialog(this.Area, this.Controller, view, this.Output);
		}
		
		public void ShowDialog()
		{
			this.ShowDialog(this.CurrentAction);
		}
		
		public void RenderView()
		{
			this.RenderView(this.CurrentAction);
		}
		
		public void Invoke(string area, string controller, string action, IView referrer, params object[] args)
		{
			FrontController.Invoke(area, controller, action, referrer, args);
		}
		
		public void Invoke(string controller, string action, IView referrer, params object[] args)
		{
			FrontController.Invoke(this.Area, controller, action, referrer, args);
		}
		
		public void Invoke(string action, IView referrer, params object[] args)
		{
			FrontController.Invoke(this.Area, this.Controller, action, referrer, args);
		}
		
		public void Invoke(string action)
		{
			FrontController.Invoke(this.Area, this.Controller, action, null, null);
		}

	}
}
