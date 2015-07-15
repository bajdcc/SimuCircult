using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Drawing;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
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
		public WireX()
		{
			_L1_line = LineElement.Create();
			_elements.Add(_L1_line);
			OnStateUpdated += WireX_OnStateUpdated;
			OnValueUpdated += WireX_OnValueUpdated;
		}

		protected virtual void WireX_OnStateUpdated(object sender, MutableStateUpdatedEventArgs e)
		{
			if (!e.Active)
			{
				switch (Local.Code)
				{
					case Constants.LOW_LEVEL:
						_L1_line[GraphicsDefines.Line_Color] = Constants.InactiveLowLevel;
						break;
					case Constants.HIGH_LEVEL:
						_L1_line[GraphicsDefines.Line_Color] = Constants.InactiveHighLevel;
						break;
					default:
						break;
				}
			}
		}

		protected virtual void WireX_OnValueUpdated(object sender, MutableValueUpdatedEventArgs<T> e)
		{
			switch (e.Status.Code)
			{
				case Constants.LOW_LEVEL:
					_L1_line[GraphicsDefines.Line_Color] = Constants.ActiveLowLevel;
					break;
				case Constants.HIGH_LEVEL:
					_L1_line[GraphicsDefines.Line_Color] = Constants.ActiveHighLevel;
					break;
				default:
					break;
			}
		}

		private List<IGraphicsElement> _elements = new List<IGraphicsElement>();

		public List<IGraphicsElement> Elements
		{
			get { return _elements; }
			set { _elements = value; }
		}

		protected LineElement _L1_line;

		public virtual void Draw(Rectangle bound)
		{
			foreach (var e in _elements)
			{
				e.GetRenderer().Render(bound);
			}
		}

		public virtual void Prepare(Rectangle bound)
		{
			_L1_line[GraphicsDefines.Gdi_Bound] = bound;
			_L1_line[GraphicsDefines.Line_PointBegin] = (Left as NodeX<T>).AbsBound.Center();
			_L1_line[GraphicsDefines.Line_PointEnd] = (Right as NodeX<T>).AbsBound.Center();
		}
	}
}
