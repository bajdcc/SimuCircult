using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Drawing;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
using SimuElectricity.Common.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	public class DisplayUnit<T, U, V> : UnitX<T, U, V>
		where T : UnitStatus, new()
		where U : NodeStatus, new()
		where V : WireStatus, new()
	{
		public DisplayUnit()
		{
			_L1_border.Enable(false);
			_L2_pixel.Enable(false);
			_L1_background = BackgroundElement.Create();
			_L1_background[GraphicsDefines.Background_Color] = Color.FromArgb(64, Constants.WindowBackground);
			_L2_text = TextElement.Create();
            _L2_text.AlignmentH = StringAlignment.Near;
			Elements.Add(_L1_border);
			Elements.Add(_L1_background);
			Elements.Add(_L2_text);
		}

		private string _display = string.Empty;

		public string Display
		{
			get { return _display; }
			set { _display = value; _L2_text[GraphicsDefines.Text_Text] = value; }
		}

		protected BackgroundElement _L1_background;
		protected TextElement _L2_text;

		public override void Prepare(Rectangle bound)
		{
			base.Prepare(bound);
			_L1_background[GraphicsDefines.Gdi_Bound] = AbsBound;
            _L2_text[GraphicsDefines.Gdi_Bound] = AbsBound;
		}

		public override void Draw(Rectangle bound)
		{
			base.Draw(bound);
			foreach (var e in Elements)
			{
				e.GetRenderer().Render(AbsBound);
			}
		}
	}
}
