﻿using SimuCircult.UI.Drawing;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Renderer
{
	public class BackgroundElementRenderer : GradientBrushRenderer<BackgroundElementRenderer, BackgroundElement>
	{
		public override void Render(Rectangle bound)
		{
			var shape = (ShapeType)this[GraphicsDefines.Background_Shape];
			var brush = this[GraphicsDefines.SolidBrush_Handle] as Brush;
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
				case GraphicsDefines.GradientBackground_Shape:
					_Destroy();
					_Create();
					break;
				default:
					break;
			}
		}
	}
}
