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

		public override ElectricStatus BreakDownTest(IMedia media, ElectricStatus elecStatus, WireStatus status, double voltage, out double current)
		{
            current = 10;
            return ElectricStatus.Resistence;            
		}

		public override void SetNodeStatus(NodeStatus status)
		{
			base.SetNodeStatus(status);
			status.NQ = 100;
            status.PQ = 100;
        }

		public override void Advance()
		{
			_status.PQ = 100;
		}
	}
}
