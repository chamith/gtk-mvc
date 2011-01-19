
using System;
namespace Gtk.Mvc
{
	[AttributeUsage (AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public sealed class FilterAttribute : Attribute
	{
		readonly Type _filterType;
		public Type FilterType {
			get {
				return this._filterType;
			}
		}
		
		public FilterAttribute (Type filterType)
		{
			this._filterType = filterType;
			
		}
	}
}
