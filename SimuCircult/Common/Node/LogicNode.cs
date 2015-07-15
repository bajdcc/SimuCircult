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
	public abstract class LogicNode<T> : CommonNode<T>
		where T : Status, new()
	{
		private Func<T, T, T> _aggregate;

		public LogicNode(Func<T, T, T> aggregate)
		{
			_aggregate = aggregate;
		}

		protected override void _FromWireToNode(IEnumerable<T> inputs)
		{
			inputs.Aggregate(_InitialSeed(Next), _aggregate);
		}

		protected abstract T _InitialSeed(T seed);

		public override void Draw(Rectangle bound)
		{
			base.Draw(bound);
		}
	}
}
