using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Drawing;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
using SimuElectricity.Common.Base;
using SimuElectricity.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	public abstract class WireX<T, U> : Wire<T, U>, IDrawing
		where T : WireStatus, new()
		where U : NodeStatus, new()
	{
		public WireX()
		{
			_L1_line = LineElement.Create();
			_L1_line[GraphicsDefines.Line_Width] = Defines.LINE_WIDTH;
			_elements.Add(_L1_line);
			OnStateUpdated += WireX_OnStateUpdated;
			OnValueUpdated += WireX_OnValueUpdated;
		}

		protected virtual void WireX_OnStateUpdated(object sender, MutableStateUpdatedEventArgs e)
		{

		}

		protected virtual void WireX_OnValueUpdated(object sender, MutableValueUpdatedEventArgs<T> e)
		{

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
			_L1_line[GraphicsDefines.Line_PointBegin] = (Left as NodeX<U, T>).AbsBound.Center();
			_L1_line[GraphicsDefines.Line_PointEnd] = (Right as NodeX<U, T>).AbsBound.Center();
			_L1_line[GraphicsDefines.LineM_BuildPoint] = null;
		}

		public virtual int Handle(HandleType type, object obj)
		{
			return -1;
		}
	}
}
