using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuElectricity.Common.Base;
using SimuElectricity.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	/// <summary>
	/// 结点
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="U"></typeparam>
	public class CommonNode<T, U> : NodeX<T, U>
		where T : NodeStatus, new()
		where U : WireStatus, new()
	{
		protected override void _FromWireToNode(IEnumerable<Wire<U, T>> inputs)
		{
			Next.PQ += inputs.Sum(a => a.Local.Q);//电荷增量
		}

		protected override void _FromNodeToWire(IEnumerable<Wire<U, T>> outputs)
		{
			var seX = Math.Sign(Local.EX);
			var seY = Math.Sign(Local.EY);
            U x = null, y = null;
            double ix = 0, iy = 0;
			foreach (var output in outputs)
			{
				var scX = Math.Sign(output.Right.Coordinate.X - Coordinate.X);
				var scY = Math.Sign(output.Right.Coordinate.Y - Coordinate.Y);				
				if (seX == scX)
				{
					double current;
					output.Next.ElecStatus = Media.BreakDownTest(output.Right.Media, Local.ElecStatus, output.Local, Math.Abs(Local.EX) * Defines.CELL_CX, out current);
                    ix = Defines.Clamp(current, 0, Defines.MAX_TRANSFER_I);
					//output.Next.Current = ix;
                    x = output.Next;
                }
                if (seY == scY)
                {
                    double current;
                    output.Next.ElecStatus = Media.BreakDownTest(output.Right.Media, Local.ElecStatus, output.Local, Math.Abs(Local.EY) * Defines.CELL_CY, out current);
                    iy = Defines.Clamp(current, 0, Defines.MAX_TRANSFER_I);
                    //output.Next.Current = iy;
                    y = output.Next;
                }                
			}
            if (x != null && y != null)
            {
                double ixy = ix + iy;
                double i = Defines.Clamp(Local.PQ, 0, ixy);
                Next.PQ -= i;
                if (i != 0)
                {
                    x.Current = ix * i / ixy;
                    y.Current = iy * i / ixy;
                }
                else
                {
                    x.Current = 0;
                    y.Current = 0;
                }                
            }
            else
            {
                if (x != null)
                {
                    x.Current = Defines.Clamp(Local.PQ, 0, ix);
                    Next.PQ -= x.Current;
                }
                else if (y !=null)
                {
                    y.Current = Defines.Clamp(Local.PQ, 0, iy);
                    Next.PQ -= y.Current;
                }
            }            

            switch (Local.ElecStatus)
			{
				case ElectricStatus.Resistence:
					if (outputs.Any(a => a.Next.ElecStatus == ElectricStatus.Ionization))
						Next.ElecStatus = ElectricStatus.Ionization;
					else
						Next.ElecStatus = ElectricStatus.Resistence;
					break;
				case ElectricStatus.Ionization:
					if (outputs.Any(a => a.Next.ElecStatus == ElectricStatus.Ionization))
						Next.ElecStatus = ElectricStatus.Conduction;
					else if (outputs.All(a => a.Next.ElecStatus == ElectricStatus.Resistence))
						Next.ElecStatus = ElectricStatus.Resistence;
					else
						Next.ElecStatus = ElectricStatus.Ionization;
					break;
				case ElectricStatus.Conduction:
					if (!outputs.Any(a => a.Next.ElecStatus == ElectricStatus.Conduction))
						Next.ElecStatus = ElectricStatus.Ionization;
					else
						Next.ElecStatus = ElectricStatus.Conduction;
					break;
				default:
					break;
			}
		}

		public override void Update()
		{
			Local.PQ += Next.PQ;
            Next.PQ = 0;
            Local.PQ = Defines.Clamp(Local.PQ, Defines.MAX_Q);
			Local.EX = Next.EX;
			Local.EY = Next.EY;
			Local.ElecStatus = Next.ElecStatus;
			Next.EX = 0;
			Next.EY = 0;
			Media.Advance();
		}
	}
}
