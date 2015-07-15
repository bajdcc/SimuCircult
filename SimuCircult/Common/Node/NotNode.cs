using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Element;
using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Node
{
	public class NotNode<T> : CommonNode<T>
		where T : Status, new()
	{
		protected override void _FromWireToNode(IEnumerable<T> inputs)
		{
			base._FromWireToNode(inputs);
			Next.Code = Constants.Inverse(Next.Code);
		}

		public override void Draw(Rectangle bound)
		{
			base.Draw(bound);
		}
	}
}
