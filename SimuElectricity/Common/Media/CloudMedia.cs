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

		public override bool BreakDownTest(IMedia media, bool breaknode, WireStatus status, double voltage, out double current)
		{
			if (media.GetId() == MediaId.M_AIR)
			{
				if (status.BreakDown || Math.Abs(voltage) > 4000)
				{
					current = Defines.Clamp(voltage, 1e20);
					return status.BreakDown ? Math.Abs(voltage) > 5000 : true;
				}
				else
				{
					current = Defines.Clamp(voltage, 5000);
					return false;
				}
			}
			if (Math.Abs(voltage) > 40000)
			{
				if (status.BreakDown)
				{
					current = Defines.Clamp(voltage, 50000);
					return true;
				}
				if (breaknode)
				{
					current = Defines.Clamp(voltage, 800);
					return true;
				}
			}
			else
			{
				current = Defines.Clamp(voltage, 6000);
				return false;
			}			
			return base.BreakDownTest(media, breaknode, status, voltage, out current);
		}

		public override void SetNodeStatus(NodeStatus status)
		{
			base.SetNodeStatus(status);
			//status.Q = -1e5 * (Defines.NRand.Next() + 1);
			status.Q = -5000;
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
