using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
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
	public enum DisplayOptionType
	{
		Active,
		Focus,
		Hover
	}

	public class DisplayUnit<T> : CommonUnit<T>
		where T : Status, new()
	{
		public DisplayUnit()
		{
			_L1_title[GraphicsDefines.Text_Text] = Constants.DisplayString;
			_L1_active = TextElement.Create();
			_L2_focus = TextElement.Create();
			_L3_hover = TextElement.Create();
			AfterElements.Add(_L1_active);
			AfterElements.Add(_L2_focus);
			AfterElements.Add(_L3_hover);
			_L1_active.AlignmentH = StringAlignment.Near;
			_L2_focus.AlignmentH = StringAlignment.Near;
			_L3_hover.AlignmentH = StringAlignment.Near;
		}

		protected TextElement _L1_active;
		protected TextElement _L2_focus;
		protected TextElement _L3_hover;

		public void SetText(DisplayOptionType type, string text)
		{
			switch (type)
			{
				case DisplayOptionType.Active:
					_L1_active[GraphicsDefines.Text_Text] = "Active: " + text ?? string.Empty;
					break;
				case DisplayOptionType.Focus:
					_L2_focus[GraphicsDefines.Text_Text] = "Focus: " + text ?? string.Empty;
					break;
				case DisplayOptionType.Hover:
					_L3_hover[GraphicsDefines.Text_Text] = "Hover: " + text ?? string.Empty;
					break;
				default:
					break;
			}
		}

		public override void Prepare(Rectangle bound)
		{
			base.Prepare(bound);
			SetDisplayText();
		}

		private void SetDisplayText()
		{
			var bound = AbsBound.NearCenter(new Size(500, 30), Direction.Up);
			bound.Offset(new Point(0, 30));
			_L1_active[GraphicsDefines.Gdi_Bound] = bound;
			bound.Offset(new Point(0, 30));
			_L2_focus[GraphicsDefines.Gdi_Bound] = bound;
			bound.Offset(new Point(0, 30));
			_L3_hover[GraphicsDefines.Gdi_Bound] = bound;
		}
	}
}
