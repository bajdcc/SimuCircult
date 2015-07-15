using SimuCircult.UI.Drawing;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Renderer
{
	public class GradientBrushRenderer<T, U> : GdiRenderer<T, U>
		where T : GraphicsRenderer<T, U>, new()
		where U : GraphicsElement<U>, new()
	{
		protected override void CreateGdiObject(Graphics graphics)
		{
			this[GraphicsDefines.GradientBrush_Handle] = new LinearGradientBrush(
				(Point)_element[GraphicsDefines.GradientBrush_PointBegin],
				(Point)_element[GraphicsDefines.GradientBrush_PointEnd],
				(Color)_element[GraphicsDefines.GradientBrush_ColorBegin],
				(Color)_element[GraphicsDefines.GradientBrush_ColorEnd]);
		}

		protected override void DestroyGdiObject(Graphics graphics)
		{
			var brush = this[GraphicsDefines.SolidBrush_Handle] as Brush;
			if (brush != null)
			{
				brush.Dispose();
			}
		}

		public override void OnElementStateChanged(int state, object value)
		{
			switch (state)
			{
				case GraphicsDefines.GradientBrush_ColorBegin:
				case GraphicsDefines.GradientBrush_ColorEnd:
					_Destroy();
					_Create();
					break;
				default:
					break;
			}
		}		
	}
}
