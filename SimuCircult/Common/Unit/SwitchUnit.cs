using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Unit
{
	public class SwitchUnit<T> : CommonUnit<T>
		where T : Status, new()
	{
		private int _power = 0;

		public int Power
		{
			get { return _power; }
			set { _power = value; }
		}

		public override void Activate()
		{

		}
	}
}
