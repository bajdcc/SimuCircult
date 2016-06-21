using SimuCircult.Common.Base;
using SimuElectricity.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SimuElectricity.Common.Media;

namespace SimuElectricity.Common.Base
{
	public class NodeStatus : Status
	{
		private double _Q = 0;

        /// <summary>
        /// 基础电荷
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

        /// <summary>
		/// 介质
		/// </summary>
	    private IMedia _Media;

	    public IMedia Media
	    {
	        get { return _Media; }
	        set { _Media = value; }
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
