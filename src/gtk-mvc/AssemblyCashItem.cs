using System;
using System.Reflection;
namespace Bizline.MVC
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

	}
}

