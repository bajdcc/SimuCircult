using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Wire
{
	public class CommonWire<T> : WireX<T>
		where T : Status, new()
	{
		protected override void _FromWireToNode(T inputs)
		{

		}

		protected override void _FromNodeToWire(T outputs)
		{

		}

		public override void Prepare(Rectangle bound)
		{
			base.Prepare(bound);
			_L1_line[GraphicsDefines.Gdi_Bound] = bound;
			_L1_line[GraphicsDefines.Line_PointBegin] = (Left as NodeX<T>).AbsBound.Center();
			_L1_line[GraphicsDefines.Line_PointEnd] = (Right as NodeX<T>).AbsBound.Center();
		}
	}
}
