using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
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
	public class CommonWire<T, U> : WireX<T, U>
		where T : WireStatus, new()
		where U : NodeStatus, new()
	{
		protected override void _FromWireToNode(U inputs)
		{
			inputs.Q = Local.Q;
		}

		protected override void _FromNodeToWire(U outputs)
		{
			Next.Q = outputs.Q * (Defines.Rand.NextDouble() + 0.5);
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
