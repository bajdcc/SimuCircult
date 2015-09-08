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

		public override ElectricStatus BreakDownTest(IMedia media, ElectricStatus elecStatus, WireStatus status, double voltage, out double current)
		{
			if (media.GetId() == _id)
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
						if (Math.Abs(voltage) > 5000)
						{
							current = 80;
							return ElectricStatus.Conduction;
						}
						if (Math.Abs(voltage) > 2000)
						{
							current = 80;
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Conduction:
						if (Math.Abs(voltage) > 5000)
						{
							current = Defines.Clamp(voltage, 2e6);
							return ElectricStatus.Conduction;
						}
						break;
					default:
						break;
				}
			}
			else if (media.GetId() == MediaId.M_AIR)
			{
				switch (elecStatus)
				{
					case ElectricStatus.Resistence:
						if (Math.Abs(voltage) > 500)
						{
							current = 10;
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Ionization:
						if (Math.Abs(voltage) > 2000)
						{
							current = Defines.Clamp(voltage, 1e6);
							return ElectricStatus.Conduction;
						}
						if (Math.Abs(voltage) > 1000)
						{
							current = Defines.Clamp(voltage, 1e3);
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Conduction:
						if (Math.Abs(voltage) > 4000)
						{
							current = Defines.Clamp(voltage, 1e6);
							return ElectricStatus.Conduction;
						}
						break;
					default:
						break;
				}
			}
			return base.BreakDownTest(media, elecStatus, status, voltage, out current);
		}

		public override void SetNodeStatus(NodeStatus status)
		{
			base.SetNodeStatus(status);
			//status.Q = -1e5 * (Defines.NRand.Next() + 1);
			status.Q = -1e8;
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
