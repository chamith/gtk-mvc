
using System;
namespace Gtk.Mvc
{
	[AttributeUsage (AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	public sealed class IgnoreFilterAttribute: Attribute
	{
		readonly Type _filterType;
		public Type FilterType {
			get {
				return this._filterType;
			}
		}
		
		public IgnoreFilterAttribute (Type filterType)
		{
			this._filterType = filterType;
			
		}		
	}
}
