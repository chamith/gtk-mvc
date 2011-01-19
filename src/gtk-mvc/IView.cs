using System;
using System.Collections;
namespace Gtk.Mvc
{
	public interface IView
	{
		ViewInputModel ParseInput();
		void RenderOutput(ViewOutputModel output);
		Hashtable Context{get; set;}
	}
}

