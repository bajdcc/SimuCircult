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
			if (media.GetId() == MediaId.M_AIR)
			{
				if (breakdown || Math.Abs(voltage) > 30 * (1 + Defines.NRand.Next()))
				{
					current = Defines.Clamp(voltage, 120000);
					return Math.Abs(voltage) > 100;
				}
				return base.BreakDownTest(media, breaknode, breakdown, voltage, out current);
			}
			if (Math.Abs(voltage) > 30)
			{
				if (breakdown)
				{
					current = Defines.Clamp(voltage, 50000);
					return true;
				}
				if (breaknode)
				{
					current = Defines.Clamp(voltage, 400);
					return true;
				}				
			}
			else
			{
				current = Defines.Clamp(voltage, 200);
				return false;
			}			
			return base.BreakDownTest(media, breaknode, breakdown, voltage, out current);
		}

		public override void SetNodeStatus(NodeStatus status)
		{
			base.SetNodeStatus(status);
			//status.Q = -1e5 * (Defines.NRand.Next() + 1);
			status.Q = -4e5;
		}

		public override void Advance()
		{
			/*if (Math.Abs(_status.Q) < 1e7)
			{
				_status.Q *= 1.7;
			}*/
		}
	}
}
