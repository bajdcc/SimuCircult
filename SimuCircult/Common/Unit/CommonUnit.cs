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
			_L1_title = TextElement.Create();
			_L2_display = TextElement.Create();
			AfterElements.Add(_L1_title);
			AfterElements.Add(_L2_display);
		}

		private string _display = string.Empty;

		public string Display
		{
			get { return _display; }
			set { _display = value; _L2_display[GraphicsDefines.Text_Text] = value; }
		}

		protected TextElement _L1_title;
		protected TextElement _L2_display;

		protected override void _FromWireToNode(IEnumerable<T> inputs)
		{
			
		}

		protected override void _FromNodeToWire(IEnumerable<T> outputs)
		{
			
		}

		public override void Prepare(Rectangle bound)
		{
			base.Prepare(bound);
			SetTitle();
		}

		protected virtual void SetTitle()
		{
			_L1_title[GraphicsDefines.Gdi_Bound] = AbsBound.NearCenter(new Size(500, 30), Direction.Up);
			_L2_display[GraphicsDefines.Gdi_Bound] = AbsBound.NearCenter(new Size(500, 30), Direction.Bottom);
		}
	}
}
