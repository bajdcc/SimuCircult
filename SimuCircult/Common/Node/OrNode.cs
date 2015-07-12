using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Node
{
	public class OrNode<T> : LogicNode<T>
		where T : Status, new()
	{
		public OrNode()
			: base((a, b) => { a.Code |= b.Code; return a; })
		{
			
		}

		protected override void _Advance(IEnumerable<T> inputs, IEnumerable<T> outputs)
		{
			
		}

		protected override T _InitialSeed(T seed)
		{
			seed.Code = 0;
			return seed;
		}
	}
}
