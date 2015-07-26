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

		public override bool BreakDownTest(IMedia media, bool breaknode, bool breakdown, double voltage, out double current)
		{
			current = voltage;
			return false;
		}

		public override void SetNodeStatus(NodeStatus status)
		{
			base.SetNodeStatus(status);
			status.Q = 0;
		}

		public override void Advance()
		{
			//_status.Q = 0;
		}
	}
}
