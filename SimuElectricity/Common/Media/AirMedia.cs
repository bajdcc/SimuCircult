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
	/// 空气介质
	/// </summary>
	public class AirMedia : MediaBase
	{
		public AirMedia()
			: base(MediaId.M_AIR)
		{

		}

		public override bool BreakDownTest(IMedia media, bool breaknode, WireStatus status, double voltage, out double current)
		{
			if (media.GetId() == _id)
			{
				if (breaknode && Math.Abs(voltage) > 1e5)
				{
					if (status.BreakDown)
					{
						current = Defines.Clamp(voltage, Math.Max(Math.Abs(status.Current), 1e50));
						return true;
					}
					if (breaknode)
					{
						current = Defines.Clamp(voltage, 1e5);
						return true;
					}
					else
					{
						current = Defines.Clamp(voltage, 1e10);
						return true;
					}
				}
			}
			else if (media.GetId() == MediaId.M_GROUND)
			{
				if (breaknode && Math.Abs(voltage) > 10000)
				{
					if (status.BreakDown)
					{
						current = Defines.Clamp(voltage, 1e5);
						return true;
					}
					if (breaknode)
					{
						current = Defines.Clamp(voltage, 1e3);
						return true;
					}
					else
					{
						current = Defines.Clamp(voltage, 1e10);
						return true;
					}
				}
			}
			else
			{
				current = Defines.Clamp(voltage, 50);
				return false;
			}
			return base.BreakDownTest(media, breaknode, status, voltage, out current);
		}

		public override void SetNodeStatus(NodeStatus status)
		{
			base.SetNodeStatus(status);
			status.Q = (Defines.NRand.Next() - 0.5) * 1e-5;
			//status.Q = 0;
		}

		public override void Advance()
		{
			/*if (Math.Abs(_status.Q) > 1e6)
			{
				_status.Q *= 0.01;
			}*/
		}

		public override double? CalculateElectricField()
		{
			return 1.1;
		}
	}
}
