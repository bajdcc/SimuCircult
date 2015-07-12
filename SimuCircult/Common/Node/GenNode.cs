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
		private int _power = 0;

		public int Power
		{
			get { return _power; }
			set { _power = value; }
		}

		protected override void _Advance(IEnumerable<T> inputs, IEnumerable<T> outputs)
		{
			outputs.Single().Code = _power;
		}

		public new void Draw()
		{
			
		}
	}
}
