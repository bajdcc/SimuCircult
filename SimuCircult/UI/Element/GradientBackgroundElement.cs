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
	/// 渐变颜色图元
	/// </summary>
	public class GradientBackgroundElement : GraphicsElement<GradientBackgroundElement>
	{
		public GradientBackgroundElement()
		{
			this[GraphicsDefines.GradientBackground_ColorBegin] = Color.Black;
			this[GraphicsDefines.GradientBackground_ColorEnd] = Color.White;
			this[GraphicsDefines.GradientBackground_PointBegin] = Point.Empty;
			this[GraphicsDefines.GradientBackground_PointEnd] = Point.Empty;
			this[GraphicsDefines.GradientBackground_Direction] = GradientType.Horizontal;
			this[GraphicsDefines.GradientBackground_Shape] = ShapeType.Rectangle;
		}
	}
}
