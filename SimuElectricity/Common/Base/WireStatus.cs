using SimuCircult.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Base
{
	public class WireStatus : Status
	{
		private double _Q = 0;

		public double Q
		{
			get { return _Q; }
			set { _Q = value; }
		}
	}
}
