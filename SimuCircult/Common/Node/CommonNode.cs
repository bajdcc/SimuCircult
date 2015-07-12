using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Node
{
	public class CommonNode<T> : NodeX<T>
		where T : Status, new()
	{
		protected override void _Advance(IEnumerable<T> inputs, IEnumerable<T> outputs)
		{
			var input = inputs.Single().Code;
			foreach (var output in outputs)
				output.Code = input;
		}

		public override void Activate()
		{

		}

		public override void Draw()
		{
			
		}
	}
}
