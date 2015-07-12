using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Wire
{
	public class CommonWire<T> : WireX<T>
		where T : Status, new()
	{
		private int _power = 0;

		public int Power
		{
			get { return _power; }
			set { _power = value; }
		}

		public override void Advance()
		{

		}

		public override void Activate()
		{

		}

		public override void Draw()
		{

		}
	}
}
