﻿using SimuCircult.Common.Base;
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
			_FromWireToNode(inputs);
			_FromNodeToWire(outputs);
		}

		protected virtual void _FromWireToNode(IEnumerable<T> inputs)
		{
			Next.Code = inputs.Single().Code;
		}

		protected virtual void _FromNodeToWire(IEnumerable<T> outputs)
		{
			outputs.AsParallel().ForAll(a => a.Code = Next.Code);
		}

		public override void Activate()
		{

		}

		public override void Draw()
		{
			
		}
	}
}
