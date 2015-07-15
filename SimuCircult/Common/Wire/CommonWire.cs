using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using SimuCircult.Common.Simulator;
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
	}
}
