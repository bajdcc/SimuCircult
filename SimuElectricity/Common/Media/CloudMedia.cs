using SimuElectricity.Common.Base;
using SimuElectricity.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuElectricity.Common.Media
{
	/// <summary>
	/// 云介质
	/// </summary>
	public class CloudMedia : MediaBase
	{
		public CloudMedia()
			: base(MediaId.M_CLOUD)
		{

		}

		public override bool BreakDownTest(IMedia media, bool breaknode, bool breakdown, double voltage, out double current)
		{
			if (Math.Abs(voltage) > 200)
			{
				if (breakdown)
				{
					current = Defines.Clamp(voltage, 500);
					return true;
				}
				if (breaknode)
				{
					current = Defines.Clamp(voltage, 20);
					return true;
				}
			}
			else
			{
				current = Defines.Clamp(voltage, 5);
				return false;
			}			
			return base.BreakDownTest(media, breaknode, breakdown, voltage, out current);
		}

		public override void SetNodeStatus(NodeStatus status)
		{
			base.SetNodeStatus(status);
			status.Q = 3e4 * (Defines.NRand.Next() * 0.01 + 0.01);
			//status.Q = -1e11;
		}

		public override void Advance()
		{
			if (Math.Abs(_status.Q) < 1e7)
			{
				_status.Q *= 1.7;
			}
		}
	}
}
