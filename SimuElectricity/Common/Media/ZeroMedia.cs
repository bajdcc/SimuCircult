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
	/// 零电位介质
	/// </summary>
	public class ZeroMedia : MediaBase
	{
		public ZeroMedia()
			: base(MediaId.M_ZERO)
		{

		}

		public override bool BreakDownTest(IMedia media, bool breaknode, WireStatus status, double voltage, out double current)
		{
			if (Math.Abs(voltage) > 4000)
			{
				if (status.BreakDown)
				{
					current = Defines.Clamp(voltage, 1e70);
					return true;
				}
				if (breaknode)
				{
					current = Defines.Clamp(voltage, 800);
					return true;
				}
			}
			return base.BreakDownTest(media, breaknode, status, voltage, out current);
		}

		public override void SetNodeStatus(NodeStatus status)
		{
			base.SetNodeStatus(status);
			status.Q = 0;
		}

		public override void Advance()
		{
			_status.Q = 0;
		}
	}
}
