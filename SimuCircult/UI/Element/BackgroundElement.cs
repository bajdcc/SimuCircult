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
	/// 背景颜色图元
	/// </summary>
	public class BackgroundElement: GraphicsElement<BackgroundElement>
	{
		public BackgroundElement()
		{
			this[GraphicsDefines.Background_Color] = Color.Black;
			this[GraphicsDefines.Background_Shape] = ShapeType.Rectangle;
		}
	}
}
