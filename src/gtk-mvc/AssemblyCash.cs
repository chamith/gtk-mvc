using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Gtk.Mvc
{
	public class AssemblyCash:KeyedCollection<string, AssemblyCashItem>
	{
		#region implemented abstract members of System.Collections.ObjectModel.KeyedCollection[System.String,AssemblyCashItem]
		protected override string GetKeyForItem (AssemblyCashItem item)
		{
			return item.Name;
		}
		
		#endregion

	}
}

