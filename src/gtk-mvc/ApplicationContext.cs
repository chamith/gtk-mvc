using System;
using System.Collections;
namespace Gtk.Mvc
{
	public class ApplicationContext:Hashtable
	{
		static ApplicationContext _appContext;
		public static ApplicationContext Current
		{
			get
			{
				if( _appContext == null)
					_appContext = new ApplicationContext();
				
				return _appContext;
			}
		}
	}
}

