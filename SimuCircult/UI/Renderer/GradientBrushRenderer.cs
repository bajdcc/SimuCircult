using SimuCircult.UI.Drawing;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Element
{
	public class GradientBrushRenderer<T, U> : BrushRenderer<T, U>
		where T : GraphicsRenderer<T, U>, new()
		where U : GraphicsElement<U>, new()
	{
		private static readonly Point _startPoint = Point.Empty;
		private static readonly Point _endPoint = new Point(1, 1);

		protected override void CreateBrush(Graphics graphics)
		{
			this[GraphicsDefines.GradientBrush_Handle] = new LinearGradientBrush(_startPoint, _endPoint, (Color)_element[GraphicsDefines.GradientBrush_ColorBegin], (Color)_element[GraphicsDefines.GradientBrush_ColorEnd]);
		}

		protected override void DestroyBrush(Graphics graphics)
		{
			var brush = this[GraphicsDefines.SolidBrush_Handle] as Brush;
			if (brush != null)
			{
				brush.Dispose();
			}
		}

		public override void OnElementStateChanged(int state)
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
