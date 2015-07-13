using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Node
{
	public class GenNode<T> : CommonNode<T>
		where T : Status, new()
	{
		protected override void _FromWireToNode(IEnumerable<T> inputs)
		{

		}

		public new void Draw()
		{
			
		}
	}
}
