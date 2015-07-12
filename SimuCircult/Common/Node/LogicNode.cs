using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Element;
using System;
using System.Collections.Generic;
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

		protected override void _Advance(IEnumerable<T> inputs, IEnumerable<T> outputs)
		{
			T result = inputs.Aggregate(_InitialSeed(new T()), _aggregate);
			outputs.AsParallel().ForAll(a => a.Code = result.Code);
		}

		protected abstract T _InitialSeed(T seed);

		public new void Draw()
		{
			
		}
	}
}
