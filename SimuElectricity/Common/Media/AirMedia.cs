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

		public override bool BreakDownTest(IMedia media, bool breaknode, bool breakdown, double voltage, out double current)
		{
			if (media.GetId() == _id)
			{
				if (Math.Abs(voltage) > 100 && breaknode)
				{
					if (breakdown)
					{
						current = Defines.Clamp(voltage, 300);
						return true;
					}
					if (breaknode)
					{
						current = Defines.Clamp(voltage, 15);
						return true;
					}
				}
				else
				{
					current = Defines.Clamp(voltage, 100) * 0.1;
					return false;
				}
			}			
			return base.BreakDownTest(media, breaknode, breakdown, voltage, out current);
		}

		public override void SetNodeStatus(NodeStatus status)
		{
			base.SetNodeStatus(status);
			status.Q = (Defines.NRand.Next() - 0.5) * 1e-4;
			//status.Q = 0;
		}

		public override void Advance()
		{
			//if (Math.Abs(_status.Q) > 1e-12)
			//{
			//	_status.Q *= 0.2;
			//}
		}

		public override double? CalculateElectricField()
		{
			return 0.1;
		}
	}
}
