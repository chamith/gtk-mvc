using System;
using System.Collections;
namespace Bizline.MVC
{
	public interface IView
	{
		ViewInputModel ParseInput();
		void RenderOutput(ViewOutputModel output);
		Hashtable Context{get; set;}
	}
}

