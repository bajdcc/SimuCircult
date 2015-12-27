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
            voltage *= 0.95;
            if (media.GetId() == _id)
			{
                switch (elecStatus)
                {
                    case ElectricStatus.Resistence:
                        current = Defines.Clamp(voltage, 80);
                        if (Math.Abs(voltage) > 120)
                        {
                            return ElectricStatus.Ionization;
                        }
                        else
                        {
                            return elecStatus;
                        }
                    case ElectricStatus.Ionization:
                        if (Math.Abs(voltage) > 450)
                        {
                            current = Defines.Clamp(voltage, 1e6);
                            return ElectricStatus.Conduction;
                        }
                        if (Math.Abs(voltage) > 120)
                        {
                            current = Defines.Clamp(voltage, 1e4);
                            return ElectricStatus.Ionization;
                        }
                        break;
                    case ElectricStatus.Conduction:
                        if (Math.Abs(voltage) > 30)
                        {
                            current = Defines.Clamp(voltage, 1e6);
                            return ElectricStatus.Conduction;
                        }
                        else
                        {
                            current = Defines.Clamp(voltage, 1e6);
                            return ElectricStatus.Ionization;
                        }
                    default:
                        break;
                }
            }
			else if (media.GetId() == MediaId.M_AIR)
			{
                switch (elecStatus)
                {
                    case ElectricStatus.Resistence:
                        current = Defines.Clamp(voltage, 0.01);
                        if (Math.Abs(voltage) > 400)
                        {
                            return ElectricStatus.Ionization;
                        }
                        else
                        {
                            return elecStatus;
                        }
                    case ElectricStatus.Ionization:
                        if (Math.Abs(voltage) > 500)
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
                        if (Math.Abs(voltage) > 50)
                        {
                            current = Defines.Clamp(voltage, 1e6);
                            return ElectricStatus.Conduction;
                        }
                        else
                        {
                            current = Defines.Clamp(voltage, 1e6);
                            return ElectricStatus.Ionization;
                        }
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
			status.NQ = 50;
            status.PQ = 0;
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
