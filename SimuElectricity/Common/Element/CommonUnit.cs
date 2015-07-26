using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
using SimuElectricity.Common.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	public class CommonUnit<T, U, V> : UnitX<T, U, V>
		where T : UnitStatus, new()
		where U : NodeStatus, new()
		where V : WireStatus, new()
	{
		public CommonUnit()
		{

		}

		public override void Update()
		{

		}

		protected override int _Click(Point pt)
		{
			foreach (var node in Nodes)
			{
				node.Local.Q += 1;
			}
			return 0;
		}

		protected override int _RightClick(Point pt)
		{
			foreach (var node in Nodes)
			{
				node.Local.Q -= 1;
			}
			return 0;
		}
	}
}
