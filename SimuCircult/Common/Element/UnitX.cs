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
	public abstract class UnitX<T> : Unit<T>, IDraw
		where T : Status, new()
	{
		private Rectangle _bound = Rectangle.Empty;

		public Rectangle Bound
		{
			get { return _bound; }
			set { _bound = value; }
		}

		private List<IGraphicsElement> _beforeElements = new List<IGraphicsElement>();

		public List<IGraphicsElement> BeforeElements
		{
			get { return _beforeElements; }
			set { _beforeElements = value; }
		}

		private List<IGraphicsElement> _afterElements = new List<IGraphicsElement>();

		public List<IGraphicsElement> AfterElements
		{
			get { return _afterElements; }
			set { _afterElements = value; }
		}

		public virtual void Draw(Rectangle bound)
		{
			var newBound = bound.AdjustBound(_bound);
			foreach (var e in _beforeElements)
			{
				e.GetRenderer().Render(newBound);
			}
			foreach (var e in Inputs.Union(Hidden).Union(Outputs).Where(a => a is IDraw))
			{
				(e as IDraw).Draw(newBound);
			}
			foreach (var e in _afterElements)
			{
				e.GetRenderer().Render(newBound);
			}
		}
	}
}
