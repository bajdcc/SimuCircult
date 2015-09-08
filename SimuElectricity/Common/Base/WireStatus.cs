using SimuCircult.Common.Base;
using SimuElectricity.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Base
{
	public class WireStatus : Status
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

		private double _current = 0;

		/// <summary>
		/// 电流
		/// </summary>
		public double Current
		{
			get { return _current; }
			set { _current = value; }
		}

		/// <summary>
		/// 状态
		/// </summary>
		public ElectricStatus ElecStatus
		{
			get
			{
				return _elecStatus;
			}

			set
			{
				_elecStatus = value;
			}
		}

		public bool Breakdown
		{
			get { return _elecStatus != ElectricStatus.Resistence; }
		}

		private ElectricStatus _elecStatus = ElectricStatus.Resistence;

		public override string ToString()
		{
			return _elecStatus.ToString();
		}
	}
}
