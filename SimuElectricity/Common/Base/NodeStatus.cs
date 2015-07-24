using SimuCircult.Common.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Base
{
	public class NodeStatus : Status
	{
		private double _Q = 0;

		public double Q
		{
			get { return _Q; }
			set { _Q = value; }
		}

		private PointF _E = PointF.Empty;

		public PointF E
		{
			get { return _E; }
			set { _E = value; }
		}
	}
}
