using SimuCircult.UI.Drawing;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Renderer
{
	public class SolidBrushRenderer<T, U> : GdiRenderer<T, U>
		where T : GraphicsRenderer<T, U>, new()
		where U : GraphicsElement<U>, new()
	{
		protected override void CreateGdiObject(Graphics graphics)
		{
			this[GraphicsDefines.SolidBrush_Handle] = new SolidBrush((Color)_element[GraphicsDefines.SolidBrush_Color]);
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
				case GraphicsDefines.SolidBrush_Color:
					_Destroy();
					_Create();
					break;
				default:
					break;
			}
		}		
	}
}
