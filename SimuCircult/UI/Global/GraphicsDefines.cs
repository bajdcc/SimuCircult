using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Global
{
	public static class GraphicsDefines
	{
		public const int Gdi_Handle = 100;
		public const int Gdi_Color = 101;
		public const int Gdi_ColorBegin = Gdi_Color;
		public const int Gdi_ColorEnd = Gdi_ColorBegin + 1;
		public const int Gdi_Shape = 109;

		public const int SolidBrush_Handle = Gdi_Handle;
		public const int SolidBrush_Color = Gdi_Color;

		public const int GradientBrush_Handle = Gdi_Handle;
		public const int GradientBrush_ColorBegin = Gdi_ColorBegin;
		public const int GradientBrush_ColorEnd = Gdi_ColorEnd;
		public const int GradientBrush_PointBegin = 141;
		public const int GradientBrush_PointEnd = 142;

		public const int Pen_Handle = Gdi_Handle;
		public const int Pen_Color = Gdi_Color;
		public const int Pen_Width = 151;
		public const int Pen_DashStyle = 152;
		public const int Pen_LineJoin = 153;		

		public const int Border_Color = Pen_Color;
		public const int Border_Width = Pen_Width;
		public const int Border_Style = Pen_DashStyle;
		public const int Border_Join = Pen_LineJoin;
		public const int Border_Shape = Gdi_Shape;

		public const int Background_Color = Gdi_Color;
		public const int Background_Shape = Gdi_Shape;

		public const int GradientBackground_ColorBegin = GradientBrush_ColorBegin;
		public const int GradientBackground_ColorEnd = GradientBrush_ColorEnd;
		public const int GradientBackground_PointBegin = GradientBrush_PointBegin;
		public const int GradientBackground_PointEnd = GradientBrush_PointEnd;
		public const int GradientBackground_Shape = Gdi_Shape;
		public const int GradientBackground_Direction = 161;
	}

	public enum ShapeType
	{
		Rectangle,
		Ellipse,
	}

	public enum GradientType
	{
		Custom,
		Horizontal,
		Vertical,
		Slash,
		Backslash,
	}
}
