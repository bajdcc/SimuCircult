﻿using SimuElectricity.Common.Base;
using SimuElectricity.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuElectricity.Common.Media
{
	/// <summary>
	/// 大地介质
	/// </summary>
	public class GroundMedia : MediaBase
	{
		public GroundMedia()
			: base(MediaId.M_GROUND)
		{

		}

		public override bool BreakDownTest(IMedia media, bool breaknode, bool breakdown, double voltage, out double current)
		{
			if (media.GetId() == _id)
			{
				current = Defines.Clamp(voltage, 1e30);
				return false;
			}
			else if (media.GetId() == MediaId.M_ZERO)
			{
				current = Defines.Clamp(voltage, 1e40);
				return false;
			}
			return base.BreakDownTest(media, breaknode, breakdown, voltage, out current);
		}

		public override void SetNodeStatus(NodeStatus status)
		{
			base.SetNodeStatus(status);
			//status.Q = 1e10 * (Defines.NRand.Next() + 0.3);
			//status.Q = 0;
			status.Q = (Defines.NRand.Next() - 0.5) * 1e-5;
		}

		public override double? CalculateElectricField()
		{
			return 1.0;
		}
	}
}
