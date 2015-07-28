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
	public class CommonNode<T, U> : NodeX<T, U>
		where T : NodeStatus, new()
		where U : WireStatus, new()
	{
		protected override void _FromWireToNode(IEnumerable<Wire<U, T>> inputs)
		{
			Next.Q += inputs.Sum(a => a.Local.Q);
		}

		protected override void _FromNodeToWire(IEnumerable<Wire<U, T>> outputs)
		{
			var seX = Math.Sign(Local.EX);
			var seY = Math.Sign(Local.EY);
			U lastWire = null;
			T lastNode = null;
			foreach (var output in outputs)
			{
				var scX = Math.Sign(output.Right.Coordinate.X - Coordinate.X);
				var scY = Math.Sign(output.Right.Coordinate.Y - Coordinate.Y);				
				if (seX == scX)
				{
					double current;
					output.Next.BreakDown = Media.BreakDownTest(output.Right.Media, Local.BreakDown, output.Local, Math.Abs(Local.EX) * Defines.CELL_CX, out current);
					current = Defines.Clamp(current, Defines.MIN_TRANSFER_I, Defines.MAX_TRANSFER_I);
					output.Next.Current = current;
					if (output.Next.BreakDown)
					{
						if (lastWire == null)
						{
							lastWire = output.Next;
							lastNode = output.Right.Next;
						}
						else
						{
							if (Math.Abs(current) > Math.Abs(lastWire.Current))
							{
								lastWire.BreakDown = false;
								lastWire.Current = 0.0;
								lastWire = output.Next;
								lastNode = output.Right.Next;
							}
						}
					}
				}
				if (seY == scY)
				{
					double current;
					output.Next.BreakDown = Media.BreakDownTest(output.Right.Media, Local.BreakDown, output.Local, Math.Abs(Local.EY) * Defines.CELL_CY, out current);
					current = Defines.Clamp(current, Defines.MIN_TRANSFER_I, Defines.MAX_TRANSFER_I);
					output.Next.Current = current;
					if (output.Next.BreakDown)
					{
						if (lastWire == null)
						{
							lastWire = output.Next;
							lastNode = output.Right.Next;
						}
						else
						{
							if (Math.Abs(current) > Math.Abs(lastWire.Current))
							{
								lastWire.BreakDown = false;
								lastWire.Current = 0.0;
								lastWire = output.Next;
								lastNode = output.Right.Next;
							}
						}
					}
				}
			}
			if (lastWire != null)
			{
				Next.BreakDown = true;
				lastNode.BreakDown = true;
			}
		}

		public override void Update()
		{
			Local.Q += Next.Q;
			Local.Q = Defines.Clamp(Local.Q, Defines.MAX_Q);
			Local.EX = Next.EX;
			Local.EY = Next.EY;
			Local.BreakDown = Next.BreakDown;
			Next.EX = 0;
			Next.EY = 0;
			Media.Advance();
		}
	}
}
