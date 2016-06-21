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
						current = Defines.Clamp(voltage, 1e3);
						if (Math.Abs(voltage) > 500)
						{
							return ElectricStatus.Ionization;
						}
                        else
                        {
                            return elecStatus;
                        }
					case ElectricStatus.Ionization:
						if (Math.Abs(voltage) > 600)
						{
							current = Defines.Clamp(voltage, 1e6);
							return ElectricStatus.Conduction;
						}
						if (Math.Abs(voltage) > 500)
						{
							current = Defines.Clamp(voltage, 1e4);
							return ElectricStatus.Ionization;
						}
						break;
					case ElectricStatus.Conduction:
                        if (Math.Abs(voltage) > 100)
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
			else if (media.GetId() == MediaId.M_GROUND)

			{
                switch (elecStatus)
                {
                    case ElectricStatus.Resistence:
                        current = Defines.Clamp(voltage, 10);
                        if (Math.Abs(voltage) > 600)
                        {
                            return ElectricStatus.Ionization;
                        }
                        else
                        {
                            return elecStatus;
                        }
                    case ElectricStatus.Ionization:
                        if (Math.Abs(voltage) > 40)
                        {
                            current = Defines.Clamp(voltage, 1e6);
                            return ElectricStatus.Conduction;
                        }
                        if (Math.Abs(voltage) > 600)
                        {
                            current = Defines.Clamp(voltage, 1e6);
                            return ElectricStatus.Ionization;
                        }
                        break;
                    case ElectricStatus.Conduction:
                        if (Math.Abs(voltage) > 5)
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
		    status.Q = 5 * Defines.NRand.Next();
		}

		public override void Advance()
		{
			/*if (Math.Abs(_status.Q) > 1e6)
			{
				_status.Q *= 0.01;
			}*/
		}

        public override double EffectiveQ()
        {
            return _status.Q;
        }
    }
}
