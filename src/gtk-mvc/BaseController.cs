
using System;
using Gtk;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Gtk.Mvc.Configuration;
namespace Gtk.Mvc
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
		public string CallbackMethod {
			get;
			set;
		}
		
		public IView CallbackView {
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
		
		public void Callback()
		{
			this.RenderView(this.CallbackView, this.CallbackMethod, this.Output);
		}
		
		public void Callback(params object[] args)
		{
			this.RenderView(this.CallbackView, this.CallbackMethod, args);
		}
		public void InvokeWithCallback(string action, IView callbackView, string callbackMethodName, params object[] args)
		{
			FrontController.InvokeWithCallback(action, this.Referrer, callbackView, callbackMethodName, args);
		}
		public void Invoke(string action, params object[] args)
		{
			FrontController.Invoke(action, this.Referrer, args);
		}

	}
}
