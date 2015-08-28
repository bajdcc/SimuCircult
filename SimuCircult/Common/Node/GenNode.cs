using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Element;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Node
{
	public class GenNode<T> : CommonNode<T>
		where T : Status, new()
	{
		public Action OnClick { set; get; }

		protected override void _FromWireToNode(IEnumerable<T> inputs)
		{

		}

		public override void Draw(Rectangle bound)
		{
			base.Draw(bound);
		}

		protected override int _Click(Point pt)
		{
			if (OnClick != null)
			{
				OnClick();
			}
			return 0;
		}
	}
}
