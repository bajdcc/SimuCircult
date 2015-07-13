using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Unit
{
	public class CommonUnit<T> : UnitX<T>
		where T : Status, new()
	{
		protected override void _Advance(IEnumerable<T> inputs, IEnumerable<T> outputs)
		{

		}

		public override void Activate()
		{
			Inputs.AsParallel().ForAll(a => a.Activate());
			Hidden.AsParallel().ForAll(a => a.Activate());
			Outputs.AsParallel().ForAll(a => a.Activate());
		}

		public override void Draw()
		{

		}
	}
}
