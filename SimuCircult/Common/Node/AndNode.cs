using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Element;
using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Node
{
	public class AndNode<T> : LogicNode<T>
		where T : Status, new()
	{
		public AndNode()
			: base((a, b) => Status._And(a, b))
		{
			
		}

		protected override T _InitialSeed(T seed)
		{
			seed.Code = Constants.HIGH_LEVEL;
			return seed;
		}
	}
}
