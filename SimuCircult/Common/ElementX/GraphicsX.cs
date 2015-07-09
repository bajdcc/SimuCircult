using SimuCircult.UI.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.ElementX
{
	class GraphicsX
	{
		private Dictionary<string, object> _dict = new Dictionary<string, object>();

		public Dictionary<string, object> Dict
		{
			get { return _dict; }
			set { _dict = value; }
		}

		private List<IGraphicsElement> _elements = new List<IGraphicsElement>();

		public List<IGraphicsElement> Elements
		{
			get { return _elements; }
			set { _elements = value; }
		}
	}
}
