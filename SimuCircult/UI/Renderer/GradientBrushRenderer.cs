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
		protected void _CreateBrush()
		{
			this[GraphicsDefines.GradientBrush_Handle] = new LinearGradientBrush(
				(Point)_element[GraphicsDefines.GradientBrush_PointBegin],
				(Point)_element[GraphicsDefines.GradientBrush_PointEnd],
				(Color)_element[GraphicsDefines.GradientBrush_ColorBegin],
				(Color)_element[GraphicsDefines.GradientBrush_ColorEnd]);
		}

		protected void _DestroyBrush()
		{
			_ReleaseHandle(GraphicsDefines.GradientBrush_Handle);
		}

		protected override void CreateGdiObject(Graphics graphics)
		{
			_CreateBrush();
		}

		protected override void DestroyGdiObject(Graphics graphics)
		{
			_DestroyBrush();
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
