using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.UI.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	public abstract class WireX<T> : Wire<T>, IDraw
		where T : Status, new()
	{
		private Rectangle _bound = Rectangle.Empty;

		public Rectangle Bound
		{
			get { return _bound; }
			set { _bound = value; }
		}

		private List<IGraphicsElement> _elements = new List<IGraphicsElement>();

		public List<IGraphicsElement> Elements
		{
			get { return _elements; }
			set { _elements = value; }
		}

		public virtual void Draw(Rectangle bound)
		{
			foreach (var e in _elements)
			{
				e.GetRenderer().Render(bound.AdjustBound(_bound));
			}
		}
	}
}
