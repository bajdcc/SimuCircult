using SimuCircult.Common.Simulator;
using SimuCircult.UI.Drawing;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Renderer
{
	public class BorderElementRenderer : PenRenderer<BorderElementRenderer, BorderElement>
	{
		protected override void _Render(Rectangle bound)
		{
			var shape = (ShapeType)_element[GraphicsDefines.Border_Shape];
			var pen = this[GraphicsDefines.Pen_Handle] as Pen;
			switch (shape)
			{
				case ShapeType.Rectangle:
					_graphics.DrawRectangle(pen, bound);
					break;
				case ShapeType.Ellipse:
					_graphics.DrawEllipse(pen, bound);
					break;
				default:
					break;
			}
		}

		public override void OnElementStateChanged(int state, object value)
		{
			switch (state)
			{
				case GraphicsDefines.Border_Color:
				case GraphicsDefines.Border_Width:
				case GraphicsDefines.Border_Style:
				case GraphicsDefines.Border_Join:
					_Destroy();
					_Create();
					break;
				case GraphicsDefines.Border_Focus:
				case GraphicsDefines.Border_Hover:
					_Adjust();
					break;
				default:
					break;
			}
		}

		private void _Adjust()
		{
			if ((bool)_element[GraphicsDefines.Border_Focus])
				_element[GraphicsDefines.Border_Color] = Constants.FocusBorder;
			else if ((bool)_element[GraphicsDefines.Border_Hover])
				_element[GraphicsDefines.Border_Color] = Constants.HoverBorder;
			else
				_element[GraphicsDefines.Border_Color] = Constants.NormalBorder;
		}
	}
}
