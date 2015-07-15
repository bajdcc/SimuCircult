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
	public class GradientBackgroundElementRenderer : GradientBrushRenderer<GradientBackgroundElementRenderer, GradientBackgroundElement>
	{
		protected override void _Render(Rectangle bound)
		{
			var shape = (ShapeType)this[GraphicsDefines.GradientBackground_Shape];
			var direction = (GradientType)this[GraphicsDefines.GradientBackground_Direction];
			switch (direction)
			{
				case GradientType.Custom:
					break;
				case GradientType.Horizontal:
					_element[GraphicsDefines.GradientBackground_PointBegin] = new Point(bound.Left, bound.Top);
					_element[GraphicsDefines.GradientBackground_PointEnd] = new Point(bound.Right, bound.Top);
					break;
				case GradientType.Vertical:
					_element[GraphicsDefines.GradientBackground_PointBegin] = new Point(bound.Left, bound.Top);
					_element[GraphicsDefines.GradientBackground_PointEnd] = new Point(bound.Left, bound.Bottom);
					break;
				case GradientType.Slash:
					_element[GraphicsDefines.GradientBackground_PointBegin] = new Point(bound.Right, bound.Top);
					_element[GraphicsDefines.GradientBackground_PointEnd] = new Point(bound.Left, bound.Bottom);
					break;
				case GradientType.Backslash:
					_element[GraphicsDefines.GradientBackground_PointBegin] = new Point(bound.Left, bound.Top);
					_element[GraphicsDefines.GradientBackground_PointEnd] = new Point(bound.Right, bound.Bottom);
					break;
				default:
					break;
			}
			var brush = this[GraphicsDefines.GradientBrush_Handle] as Brush;
			switch (shape)
			{
				case ShapeType.Rectangle:
					_graphics.FillRectangle(brush, bound);
					break;
				case ShapeType.Ellipse:
					_graphics.FillEllipse(brush, bound);
					break;
				default:
					break;
			}
		}

		public override void OnElementStateChanged(int state, object value)
		{
			switch (state)
			{
				case GraphicsDefines.GradientBackground_ColorBegin:
				case GraphicsDefines.GradientBackground_ColorEnd:
				case GraphicsDefines.GradientBackground_PointBegin:
				case GraphicsDefines.GradientBackground_PointEnd:
				case GraphicsDefines.GradientBackground_Shape:
					_Destroy();
					_Create();
					break;
				case GraphicsDefines.GradientBackground_Direction:
					if ((GradientType)value == GradientType.Custom)
					{
						_Destroy();
						_Create();
					}
					break;
				default:
					break;
			}
		}
	}
}
