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

		public override ElectricStatus BreakDownTest(IMedia media, ElectricStatus elecStatus, WireStatus status, double voltage, out double current)
		{
			if (media.GetId() == _id)
			{
				switch (elecStatus)
				{
					case ElectricStatus.Resistence:
						if (Math.Abs(voltage) > 1000)
						{
							current = 0;
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
							current = 80;
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Conduction:
						if (Math.Abs(voltage) > 1e4)
						{
							current = Defines.Clamp(voltage, 1e5);
							return ElectricStatus.Conduction;
						}
						break;
					default:
						break;
				}
			}
			else if (media.GetId() == MediaId.M_ZERO)
			{
				switch (elecStatus)
				{
					case ElectricStatus.Resistence:
						if (Math.Abs(voltage) > 1000)
						{
							current = 0;
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Ionization:
						if (Math.Abs(voltage) > 2000)
						{
							current = 80;
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Conduction:
						if (Math.Abs(voltage) > 1e5)
						{
							current = Defines.Clamp(voltage, 1e7);
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
						if (Math.Abs(voltage) > 10000)
						{
							current = 10;
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Ionization:
						if (Math.Abs(voltage) > 20000)
						{
							current = 50;
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Conduction:
						if (Math.Abs(voltage) > 20000)
						{
							current = Defines.Clamp(voltage, 2e4);
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
			//status.Q = 1e10 * (Defines.NRand.Next() + 0.3);
			//status.Q = 0;
			status.Q = 1e2;
		}

		public override double? CalculateElectricField()
		{
			return 1.0;
		}
	}
}
