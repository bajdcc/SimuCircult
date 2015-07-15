using SimuCircult.UI.Drawing;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Renderer
{
	public class PenRenderer<T, U> : GdiRenderer<T, U>
		where T : GraphicsRenderer<T, U>, new()
		where U : GraphicsElement<U>, new()
	{
		protected override void CreateGdiObject(Graphics graphics)
		{
			var pen = new Pen((Color)_element[GraphicsDefines.Pen_Color], (float)_element[GraphicsDefines.Pen_Width]);
			pen.DashStyle = (DashStyle)_element[GraphicsDefines.Pen_DashStyle];
			pen.LineJoin = (LineJoin)_element[GraphicsDefines.Pen_LineJoin];
			this[GraphicsDefines.Pen_Handle] = pen;			
		}

		protected override void DestroyGdiObject(Graphics graphics)
		{
			_ReleaseHandle(GraphicsDefines.Pen_Handle);
		}

		public override void OnElementStateChanged(int state, object value)
		{
			switch (state)
			{
				case GraphicsDefines.Pen_Color:
				case GraphicsDefines.Pen_Width:
				case GraphicsDefines.Pen_DashStyle:
				case GraphicsDefines.Pen_LineJoin:
					_Destroy();
					_Create();
					break;
				default:
					break;
			}
		}
	}
}
