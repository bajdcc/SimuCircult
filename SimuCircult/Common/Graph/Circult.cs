using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using SimuCircult.Common.Node;
using SimuCircult.Common.Simulator;
using SimuCircult.Common.Unit;
using SimuCircult.Common.Wire;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Graph
{
	public class Circult : CircultBase<Status, CommonNode<Status>, CommonWire<Status>, CommonUnit<Status>>
	{
		public void Draw()
		{
			var bound = new Rectangle(Point.Empty, Storage.Size);
			_Prepare(bound);
			_Draw(bound);
		}

		private void _Prepare(Rectangle bound)
		{
			Units.Values.AsParallel().ForAll(a => a.Prepare(bound));
			Wires.Values.AsParallel().ForAll(a => a.Prepare(bound));
		}

		private void _Draw(Rectangle bound)
		{
			Storage.Graphics.Clear(Constants.WindowBackground);
			foreach (var wire in Wires.Values)
			{
				wire.Draw(bound);
			}
			foreach (var unit in Units.Values)
			{
				unit.Draw(bound);
			}
		}
	}
}
