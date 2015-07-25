﻿using SimuCircult.Common.Base;
using SimuElectricity.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Base
{
	public class NodeStatus : Status
	{
		private double _Q = Defines.NRand.Next() * 10;

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

		public override void CopyFrom(Status obj)
		{
			var _obj = obj as NodeStatus;
			_E = _obj._E;
			_Q = _obj._Q;
		}
	}
}
