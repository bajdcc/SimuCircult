using SimuCircult.Common.Base;
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
		private double _Q = 0;

		/// <summary>
		/// 电荷
		/// </summary>
		public double Q
		{
			get { return _Q; }
			set { _Q = value; }
		}

		private double _EX = 0;

		/// <summary>
		/// 水平方向电场强度
		/// </summary>
		public double EX
		{
			get { return _EX; }
			set { _EX = value; }
		}

		private double _EY = 0;

		/// <summary>
		/// 垂直方向电场强度
		/// </summary>
		public double EY
		{
			get { return _EY; }
			set { _EY = value; }
		}

		private bool _breakDown = false;

		/// <summary>
		/// 击穿
		/// </summary>
		public bool BreakDown
		{
			get { return _breakDown; }
			set { _breakDown = value; }
		}

		public override string ToString()
		{
			return _Q.ToString();
		}
	}
}
