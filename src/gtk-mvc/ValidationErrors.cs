using System;
using System.Collections.Specialized;
using System.Text;
namespace Bizline.MVC
{
	public class ValidationErrors:StringCollection
	{
		public ValidationErrors ():base()
		{
		}
		
		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder();
			foreach(string error in this)
				sb.AppendFormat("{0}\n", error);
			
			return sb.ToString();

		}
	}
}

