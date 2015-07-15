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
			var shape = (ShapeType)this[GraphicsDefines.Border_Shape];
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
				default:
					break;
			}
		}
	}
}
