using SimuCircult.UI.Drawing;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Element
{
	public class SolidBrushRenderer<T, U> : BrushRenderer<T, U>
		where T : GraphicsRenderer<T, U>, new()
		where U : GraphicsElement<U>, new()
	{
		protected override void CreateBrush(Graphics graphics)
		{
			this[GraphicsDefines.SolidBrush_Handle] = new SolidBrush((Color)_element[GraphicsDefines.SolidBrush_Color]);
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
