using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	public class CommonWire<T, U> : WireX<T, U>
		where T : Status, new()
		where U : Status, new()
	{
		protected override void _FromWireToNode(U inputs)
		{

		}

		protected override void _FromNodeToWire(U outputs)
		{

		}

		public override void Prepare(Rectangle bound)
		{
			base.Prepare(bound);
			_L1_line[GraphicsDefines.Gdi_Bound] = bound;
			_L1_line[GraphicsDefines.Line_PointBegin] = (Left as NodeX<U, T>).AbsBound.Center();
			_L1_line[GraphicsDefines.Line_PointEnd] = (Right as NodeX<U, T>).AbsBound.Center();
		}
	}
}
