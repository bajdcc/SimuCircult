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
			switch (elecStatus)
			{
				case ElectricStatus.Resistence:
					if (Math.Abs(voltage) > 1000)
					{
						current = 10;
						return ElectricStatus.Ionization;
					}
					break;
				case ElectricStatus.Ionization:
					if (Math.Abs(voltage) > 1e4)
					{
						current = 10;
						return ElectricStatus.Conduction;
					}
					if (Math.Abs(voltage) > 2000)
					{
						current = 800;
						return ElectricStatus.Ionization;
					}
					break;
				case ElectricStatus.Conduction:
					if (Math.Abs(voltage) > 1e4)
					{
						current = Defines.Clamp(voltage, 1e15);
						return ElectricStatus.Conduction;
					}
					break;
				default:
					break;
			}			
			return base.BreakDownTest(media, elecStatus, status, voltage, out current);
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
