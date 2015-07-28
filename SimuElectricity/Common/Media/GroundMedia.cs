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
	/// 大地介质
	/// </summary>
	public class GroundMedia : MediaBase
	{
		public GroundMedia()
			: base(MediaId.M_GROUND)
		{

		}

		public override bool BreakDownTest(IMedia media, bool breaknode, WireStatus status, double voltage, out double current)
		{
			if (media.GetId() == _id)
			{
				if (Math.Abs(voltage) > 1000)
				{
					if (status.BreakDown)
					{
						current = Defines.Clamp(voltage, 1e10);
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
					current = Defines.Clamp(voltage, 100);
				}
			}
			else if (media.GetId() == MediaId.M_ZERO)
			{
				if (Math.Abs(voltage) > 10000)
				{
					if (status.BreakDown)
					{
						current = Defines.Clamp(voltage, 1e30);
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
					current = Defines.Clamp(voltage, 10);
				}
			}
			else if (media.GetId() == MediaId.M_AIR)
			{
				if (Math.Abs(voltage) > 20000)
				{
					current = Defines.Clamp(voltage, 1e5);
					return true;
				}
			}
			return base.BreakDownTest(media, breaknode, status, voltage, out current);
		}

		public override void SetNodeStatus(NodeStatus status)
		{
			base.SetNodeStatus(status);
			//status.Q = 1e10 * (Defines.NRand.Next() + 0.3);
			//status.Q = 0;
			status.Q = Defines.NRand.Next() * 1e-5;
		}

		public override double? CalculateElectricField()
		{
			return 1.0;
		}
	}
}
