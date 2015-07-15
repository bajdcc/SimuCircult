using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Unit
{
	public class CommonUnit<T> : UnitX<T>
		where T : Status, new()
	{
		protected override void _FromWireToNode(IEnumerable<T> inputs)
		{
			
		}

		protected override void _FromNodeToWire(IEnumerable<T> outputs)
		{
			
		}
	}
}
