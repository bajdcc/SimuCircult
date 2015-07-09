using SimuCircult.UI.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Global
{
	static sealed class Storage
	{
		static private Dictionary<string, IGraphicsRendererFactory> _rendererFactory = new Dictionary<string, IGraphicsRendererFactory>();
		static public Dictionary<string, IGraphicsRendererFactory> RendererFactory
		{
			get { return _rendererFactory; }
			set { _rendererFactory = value; }
		}

		static private Dictionary<string, IGraphicsElementFactory> _elementFactory = new Dictionary<string, IGraphicsElementFactory>();
		static public Dictionary<string, IGraphicsElementFactory> ElementFactory
		{
			get { return Storage._elementFactory; }
			set { Storage._elementFactory = value; }
		}
	}
}
