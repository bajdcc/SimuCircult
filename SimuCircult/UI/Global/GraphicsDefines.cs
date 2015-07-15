using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Global
{
	public static class GraphicsDefines
	{
		public const int SolidBrush_Handle = 100;
		public const int SolidBrush_Color = 101;

		public const int GradientBrush_Handle = 110;
		public const int GradientBrush_ColorBegin = 112;
		public const int GradientBrush_ColorEnd = 113;

		public const int Pen_Handle = 150;
		public const int Pen_Color = 150;
		public const int Pen_Width = 151;
		public const int Pen_DashStyle = 152;
		public const int Pen_LineJoin = 153;

		public const int Border_Color = Pen_Color;
		public const int Border_Width = Pen_Width;
		public const int Border_Style = Pen_DashStyle;
		public const int Border_Join = Pen_LineJoin;
		public const int Border_Shape = 159;
	}
}
