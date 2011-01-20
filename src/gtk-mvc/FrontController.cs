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
	public class FrontController
	{
		public static ApplicationContext ApplicationContext { get; private set; }

		public static AssemblyCash _assemblyCash;
		static FrontController ()
		{
			_assemblyCash = new AssemblyCash ();
			
			System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration (ConfigurationUserLevel.None);
			MVCConfiguration mvcConfig = (MVCConfiguration)config.GetSection ("gtk.mvc");
			
			for (int i = 0; i < mvcConfig.Assemblies.Count; i++) {
				AssemblyConfiguration assConfig = mvcConfig.Assemblies[i] as AssemblyConfiguration;
				AssemblyCashItem cashItem = new AssemblyCashItem { Name = assConfig.Name, ControllerRootNamespace = assConfig.ControllerRootNamespace, ViewRootNamespace = assConfig.ViewRootNamespace };
				
				cashItem.Assembly = Assembly.Load (assConfig.AssemblyName);
				_assemblyCash.Add (cashItem);
			}
			
			FrontController.ApplicationContext = new ApplicationContext ();
		}

		public static BaseController CreateControllerInstance (string typeFullName)
		{
			BaseController instance = null;
			
			foreach (AssemblyCashItem cashItem in _assemblyCash) {
				instance = cashItem.Assembly.CreateInstance (cashItem.ControllerRootNamespace + "." + typeFullName, true) as BaseController;
				if (instance != null)
					return instance;
			}
			
			return instance;
		}

		public static IView CreateViewInstance (string typeFullName)
		{
			IView instance = null;
			
			foreach (AssemblyCashItem cashItem in _assemblyCash) {
				instance = cashItem.Assembly.CreateInstance (cashItem.ViewRootNamespace + "." + typeFullName, true) as IView;
				if (instance != null)
					return instance;
			}
			
			return instance;
		}

		public static BaseController GetController (string area, string controller)
		{
			string typeFullName = null;
			
			if (string.IsNullOrEmpty (area))
				typeFullName = string.Format ("{0}Controller", controller);
			else
				typeFullName = string.Format ("{0}.{1}Controller", area, controller);
			
			return CreateControllerInstance (typeFullName) as BaseController;
		}

		public static IView GetView (string area, string controller, string view)
		{
			IView viewObj = null;
			string typeFullName = null;
			
			if (string.IsNullOrEmpty (area))
				typeFullName = string.Format ("{0}.{1}", controller, view);
			else
				typeFullName = string.Format ("{0}.{1}.{2}", area, controller, view);
			
			return CreateViewInstance (typeFullName) as IView;
		}

		public static void RenderView (string area, string controller, string view, ViewOutputModel output)
		{
			IView viewObj = GetView (area, controller, view);
			
			if (viewObj == null)
				throw new ArgumentNullException (string.Format ("{0}.{1}.{2}", area, controller, view), string.Format ("View {0}.{1}.{2} cannot be found.", area, controller, view));
			
			//(viewObj as Widget).Show();
			
			viewObj.Context = output;
			viewObj.RenderOutput (output);
			
			if (viewObj is Window) {
				Window window = viewObj as Window;
				window.Maximize ();
			}
		}
		public static void RenderView (IView view, string method, params object[] args)
		{
			view.GetType ().InvokeMember (method, BindingFlags.InvokeMethod | BindingFlags.IgnoreCase, null, view, args);
		}
		public static void Rerender (IView view, ViewOutputModel output)
		{
			view.RenderOutput (output);
		}

		public static void RunDialog (string area, string controller, string view, ViewOutputModel output)
		{
			IView viewObj = GetView (area, controller, view);
			
			if (viewObj == null)
				throw new ArgumentNullException (string.Format ("{0}.{1}.{2}", area, controller, view), string.Format ("View {0}.{1}.{2} cannot be found.", area, controller, view));
			
			
			
			//(viewObj as Widget).Show();
			viewObj.Context = output;
			viewObj.RenderOutput (output);
			
			(viewObj as Dialog).Run ();
		}
		public static void RenderWidget (string area, string controller, string view, Gtk.Container container, ViewOutputModel output)
		{
			IView viewObj = GetView (area, controller, view);
			
			RenderWidget (viewObj, container, output);
			
		}

		public static void RenderWidget (IView viewObj, Gtk.Container container, ViewOutputModel output)
		{
			for (int i = 0; i < container.Children.Length;)
				container.Remove (container.Children[i]);
			
			if (viewObj == null)
				throw new ArgumentNullException ("View not found");
			
			(viewObj as Widget).Show ();
			
			viewObj.Context = output;
			viewObj.RenderOutput (output);
			
			container.Child = viewObj as Widget;
			
		}


		/// <summary>
		/// Invokes the specified action in a given controller.
		/// </summary>
		/// <param name="controller">
		/// A <see cref="System.String"/> specifying the name of the controller.
		/// </param>
		/// <param name="action">
		/// A <see cref="System.String"/> specifying the name of the action.
		/// </param>
		/// <param name="referrer">
		/// A <see cref="IView"/> representing the referrer of the action.
		/// </param>
		/// <param name="args">
		/// A <see cref="params object[]"/> containing the arguments to be passed to the action.
		/// </param>
//		public static void Invoke(string area, string controller, string action, IView referrer, string callback, params object[] args)
//		{			
//			BaseController controllerObj = GetController(area, controller);
//			
//			if(controllerObj == null)
//				throw new ArgumentNullException(
//				                                string.Format("{0}.{1}", area, controller), 
//				                                string.Format("{0}.{1}Controller cannot be found.", controller, area)
//				                                );
//			
//			controllerObj.Area = area;
//			controllerObj.Controller = controller;
//			controllerObj.CurrentAction = action;
//			controllerObj.CallbackView = referrer;
//			controllerObj.CallbackMethod = callback;
//			controllerObj.Output = new ViewOutputModel();
//			controllerObj.Request = new ActionRequest()
//			{
//				View = referrer
//			};
//			
//			MethodInfo methodInfo;
//			
//			if(args == null)
//				methodInfo = controllerObj.GetType().GetMethod(action);
//			else
//			{
//				Type[] paramTypes = new Type[args.Length];
//				for(int i=0;i<paramTypes.Length;i++)
//				paramTypes[i] = args[i].GetType();
//				
//				methodInfo = controllerObj.GetType().GetMethod(action, paramTypes);
//			}
//				
//			FilterResult result = ExecuteFilters(controllerObj, methodInfo);
//			if(result != FilterResult.CancelAction) methodInfo.Invoke(controllerObj, args);
//		}

		public static void InvokeWithCallback (string action, IView referrer, IView callbackView, string callbackMethodName, params object[] args)
		{
			string[] actionElements = action.Split ('/');
			int elementCount = actionElements.Length;
			
			if (elementCount < 2)
				throw new ArgumentException ("action string is incomplete");
			
			string actionElement = actionElements[elementCount - 1];
			string controllerElement = actionElements[elementCount - 2];
			string areaElement = string.Empty;
			if (elementCount > 2)
				areaElement = actionElements[elementCount - 3];
			
			
			BaseController controllerObj = GetController (areaElement, controllerElement);
			
			if (controllerObj == null)
				throw new ArgumentNullException (string.Format ("{0}.{1}", areaElement, controllerElement), string.Format ("{0}.{1}Controller cannot be found.", areaElement, controllerElement));
			
			controllerObj.Area = areaElement;
			controllerObj.Controller = controllerElement;
			controllerObj.CurrentAction = actionElement;
			controllerObj.CallbackView = callbackView;
			controllerObj.CallbackMethod = callbackMethodName;
			controllerObj.Output = new ViewOutputModel ();
			controllerObj.Request = new ActionRequest { View = referrer };
			
			MethodInfo methodInfo;
			
			if (args == null)
				methodInfo = controllerObj.GetType ().GetMethod (actionElement);
			else {
				Type[] paramTypes = new Type[args.Length];
				for (int i = 0; i < paramTypes.Length; i++)
					paramTypes[i] = args[i].GetType ();
				
				methodInfo = controllerObj.GetType ().GetMethod (actionElement, paramTypes);
			}
			
			FilterResult result = ExecuteFilters (controllerObj, methodInfo);
			if (result != FilterResult.CancelAction)
				methodInfo.Invoke (controllerObj, args);
			
			
		}

		public static void Invoke (string action, IView referrer, params object[] args)
		{
			InvokeWithCallback (action, referrer, null, null, args);
		}
		private static FilterResult ExecuteFilters (BaseController controllerObj, MethodInfo methodInfo)
		{
			
			object[] ignoredAttribs = methodInfo.GetCustomAttributes (typeof(IgnoreFilterAttribute), false);
			FilterResult result = FilterResult.Continue;
			object[] filterAttribs = controllerObj.GetType ().GetCustomAttributes (typeof(FilterAttribute), true);
			foreach (FilterAttribute filterAttrib in filterAttribs) {
				bool skip = false;
				foreach (IgnoreFilterAttribute ignored in ignoredAttribs) {
					skip = (filterAttrib.FilterType == ignored.FilterType);
					break;
				}
				
				if (skip)
					continue;
				
				BaseFilter filter = Activator.CreateInstance (filterAttrib.FilterType) as BaseFilter;
				result = filter.Execute ();
				
				if (result == FilterResult.CancelAction || result == FilterResult.SkipOtherFilters)
					return result;
			}
			
			return result;
		}
		
	}
	
	
}

