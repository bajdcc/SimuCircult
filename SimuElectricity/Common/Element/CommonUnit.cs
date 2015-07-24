using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	public class CommonUnit<T, U, V> : UnitX<T, U, V>
		where T : Status, new()
		where U : Status, new()
		where V : Status, new()
	{
		public CommonUnit()
		{

		}
	}
}
