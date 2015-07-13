using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using SimuCircult.Common.Node;
using SimuCircult.Common.Unit;
using SimuCircult.Common.Wire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Graph
{
	public class Circult : CircultBase<Status, CommonNode<Status>, CommonWire<Status>, CommonUnit<Status>>
	{
		public static const int LOW_LEVEL = 0;
		public static const int HIGH_LEVEL = 1;
	}
}
