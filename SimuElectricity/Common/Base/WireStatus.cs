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

		private double _current = 0;

		public double Current
		{
			get { return _current; }
			set { _current = value; }
		}

		private bool _breakDown = false;

		public bool BreakDown
		{
			get { return _breakDown; }
			set { _breakDown = value; }
		}

		public override string ToString()
		{
			return _breakDown.ToString();
		}
	}
}
