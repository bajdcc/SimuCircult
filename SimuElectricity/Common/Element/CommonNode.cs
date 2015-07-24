using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	public class CommonNode<T, U> : NodeX<T, U>
		where T : Status, new()
		where U : Status, new()
	{
		protected override void _FromWireToNode(IEnumerable<U> inputs)
		{
			
		}

		protected override void _FromNodeToWire(IEnumerable<U> outputs)
		{
			
		}
	}
}
