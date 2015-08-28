using SimuCircult.Common.Simulator;
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
	/// 边框图元
	/// </summary>
	public class BorderElement: GraphicsElement<BorderElement>
	{
		public BorderElement()
		{
			this[GraphicsDefines.Border_Color] = Constants.NormalBorder;
			this[GraphicsDefines.Border_Width] = 1.6f;
			this[GraphicsDefines.Border_Style] = DashStyle.Solid;
			this[GraphicsDefines.Border_Join] = LineJoin.Round;
			this[GraphicsDefines.Border_Shape] = ShapeType.Rectangle;
			this[GraphicsDefines.Border_Focus] = false;
			this[GraphicsDefines.Border_Hover] = false;
		}
	}
}
