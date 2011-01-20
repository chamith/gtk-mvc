using System;
using System.Reflection;
namespace Gtk.Mvc
{
	public class AssemblyCashItem
	{
		public Assembly Assembly {
			get;
			set;
		}
		
		public string Name {
			get;
			set;
		}
		
		public string ViewRootNamespace {
			get;
			set;
		}
		
		public string ControllerRootNamespace {
			get;
			set;
		}

		public string RootArea {
			get;
			set;
		}
	}
}

