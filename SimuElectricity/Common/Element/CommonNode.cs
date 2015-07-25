using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuElectricity.Common.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	public class CommonNode<T, U> : NodeX<T, U>
		where T : NodeStatus, new()
		where U : WireStatus, new()
	{
		protected override void _FromWireToNode(IEnumerable<U> inputs)
		{
			Next.Q = inputs.Sum(a => a.Q);
		}

		protected override void _FromNodeToWire(IEnumerable<U> outputs)
		{
			foreach (var output in outputs)
			{
				output.Q = Local.Q;
			}
		}
	}
}
