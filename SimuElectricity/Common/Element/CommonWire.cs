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
	/// <summary>
	/// 结点间连线
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="U"></typeparam>
	public class CommonWire<T, U> : WireX<T, U>
		where T : WireStatus, new()
		where U : NodeStatus, new()
	{
		protected override void _FromWireToNode(U inputs)
		{
			
		}

		protected override void _FromNodeToWire(U outputs)
		{
			Next.Q = Next.Current * Defines.TIME_STEP;//电荷增量
			Next.Q = Defines.Clamp(Next.Q, Defines.MIN_TRANSFER_Q, Defines.MAX_TRANSFER_Q);
			outputs.Q -= Next.Q;//电荷输出，自身电荷减少
		}

		public override void Update()
		{
			Local.Q = Next.Q;
			Next.Q = 0;
			Local.Current = Next.Current;
			Local.ElecStatus = Next.ElecStatus;
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
