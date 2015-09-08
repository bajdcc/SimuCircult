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

		public override ElectricStatus BreakDownTest(IMedia media, ElectricStatus elecStatus, WireStatus status, double voltage, out double current)
		{
			if (media.GetId() == _id)
			{
				switch (elecStatus)
				{
					case ElectricStatus.Resistence:
						if (Math.Abs(voltage) > 100)
						{
							current = 10;
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Ionization:
						if (Math.Abs(voltage) > 1e5)
						{
							current = Defines.Clamp(voltage, 1e6);
							return ElectricStatus.Conduction;
						}
						if (Math.Abs(voltage) > 200)
						{
							current = Defines.Clamp(voltage, 1e4);
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Conduction:
						if (Math.Abs(voltage) > 1e5)
						{
							current = Defines.Clamp(voltage, 1e6);
							return ElectricStatus.Conduction;
						}
						break;
					default:
						break;
				}
			}
			else if (media.GetId() == MediaId.M_GROUND)
			{
				switch (elecStatus)
				{
					case ElectricStatus.Resistence:
						if (Math.Abs(voltage) > 100)
						{
							current = 10;
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Ionization:
						if (Math.Abs(voltage) > 2e4)
						{
							current = 10;
							return ElectricStatus.Conduction;
						}
						if (Math.Abs(voltage) > 200)
						{
							current = 10;
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Conduction:
						if (Math.Abs(voltage) > 2e4)
						{
							current = Defines.Clamp(voltage, 5e5);
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
	}
}
