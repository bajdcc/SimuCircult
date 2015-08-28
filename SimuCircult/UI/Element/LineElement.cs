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
	/// <summary>
	/// 连线（折线）图元
	/// </summary>
	public class LineElement : GraphicsElement<LineElement>
	{
		public LineElement()
		{
			this[GraphicsDefines.Line_Color] = Color.Black;
			this[GraphicsDefines.Line_Width] = 1.0f;
			this[GraphicsDefines.Line_Style] = DashStyle.Solid;
			this[GraphicsDefines.Line_Join] = LineJoin.Round;
			this[GraphicsDefines.Line_PointBegin] = Point.Empty;
			this[GraphicsDefines.Line_PointEnd] = Point.Empty;
			this[GraphicsDefines.Line_PointBeginInternCount] = 0;
			this[GraphicsDefines.Line_PointEndInternCount] = 0;
		}
	}
}
