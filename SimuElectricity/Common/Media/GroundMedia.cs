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
                voltage *= 0.5;
                switch (elecStatus)
                {
                    case ElectricStatus.Resistence:
                        current = Defines.Clamp(voltage, 1e4);
                        return elecStatus;              
                    default:
                        break;
                }
            }
			else if (media.GetId() == MediaId.M_AIR)
			{
                voltage *= 0.01;
                switch (elecStatus)
                {
                    case ElectricStatus.Resistence:
                        current = Defines.Clamp(voltage, 0.01);
                        if (Math.Abs(voltage) > 200)
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
                        if (Math.Abs(voltage) > 60)
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
			//status.Q = 1e10 * (Defines.NRand.Next() + 0.3);
			//status.Q = 0;
			status.NQ = 5;
            status.PQ = 5;
        }
	}
}
