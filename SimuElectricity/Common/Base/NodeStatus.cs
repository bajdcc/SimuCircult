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
		private double _NQ = 0;

		/// <summary>
		/// 基础负电荷
		/// </summary>
		public double NQ
		{
			get { return _NQ; }
			set { _NQ = value; }
		}

        private double _PQ = 0;

        /// <summary>
        /// 基础正电荷
        /// </summary>
        public double PQ
        {
            get { return _PQ; }
            set { _PQ = value; }
        }

        /// <summary>
        /// 基础电荷
        /// </summary>
        public double Q
        {
            get { return PQ - NQ; }
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

		private ElectricStatus _elecStatus = ElectricStatus.Resistence;

		public override string ToString()
		{
			return Q.ToString();
		}
	}
}
