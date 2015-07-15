using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Unit
{
	public class CommonUnit<T> : UnitX<T>
		where T : Status, new()
	{
		public CommonUnit()
		{
			_L1_text = TextElement.Create();
			AfterElements.Add(_L1_text);
		}

		protected TextElement _L1_text;

		protected override void _FromWireToNode(IEnumerable<T> inputs)
		{
			
		}

		protected override void _FromNodeToWire(IEnumerable<T> outputs)
		{
			
		}

		public override void Prepare(Rectangle bound)
		{
			base.Prepare(bound);
			_L1_text[GraphicsDefines.Gdi_Bound] = AbsBound.NearCenter(new Size(80, 30), Direction.Up);
		}
	}
}
