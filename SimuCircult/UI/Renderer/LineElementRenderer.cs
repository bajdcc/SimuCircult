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
	public class LineElementRenderer : PenRenderer<LineElementRenderer, LineElement>
	{
		protected override void _Render(Rectangle bound)
		{
			_graphics.DrawLine(
				this[GraphicsDefines.Line_Handle] as Pen,
				(Point)_element[GraphicsDefines.Line_PointBegin],
				(Point)_element[GraphicsDefines.Line_PointEnd]
				);
		}

		public override void OnElementStateChanged(int state, object value)
		{
			switch (state)
			{
				case GraphicsDefines.Line_Color:
				case GraphicsDefines.Line_Width:
				case GraphicsDefines.Line_Style:
				case GraphicsDefines.Line_Join:
					_Destroy();
					_Create();
					break;
				default:
					break;
			}
		}
	}
}
