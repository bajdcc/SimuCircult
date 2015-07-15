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
	public class GradientBackgroundElement: GraphicsElement<BackgroundElement>
	{
		public GradientBackgroundElement()
		{
			this[GraphicsDefines.GradientBackground_ColorBegin] = Color.Black;
			this[GraphicsDefines.GradientBackground_ColorEnd] = Color.White;
			this[GraphicsDefines.GradientBackground_Shape] = ShapeType.Rectangle;
		}
	}
}
