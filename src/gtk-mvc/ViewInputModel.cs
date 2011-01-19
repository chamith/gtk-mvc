using System;
using System.Collections;
namespace Bizline.MVC
{
	public class ViewInputModel:Hashtable
	{
		ValidationErrors _validationErrors;
		
		public ViewInputModel ()
		{
			this._validationErrors = new ValidationErrors();
		}
		
		public ValidationErrors ValidationErrors
		{
			get
			{
				return this._validationErrors;
			}
		}
		
		public bool IsValid
		{
			get
			{
				return this._validationErrors.Count == 0;
			}
		}
	}
}

