using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Global
{
	public static class GraphicsDefines
	{
		public const int Gdi_Handle = 10;
		public const int Gdi_Color = 101;
		public const int Gdi_ColorBegin = Gdi_Color;
		public const int Gdi_ColorEnd = Gdi_ColorBegin + 1;
		public const int Gdi_Shape = 109;
		public const int Gdi_Bound = 110;
		public const int Gdi_PointBegin = 120;
		public const int Gdi_PointEnd = Gdi_PointBegin + 1;

		public const int Gdi_Focus = 140;
		public const int Gdi_Hover = 141;

		public const int Attr_MessageAddress = 20000;
		public const int AttrX_MessageLength = 1000;

		public const int SolidBrush_Handle = Gdi_Handle + 1;
		public const int SolidBrush_Color = Gdi_Color;

		public const int GradientBrush_Handle = Gdi_Handle + 2;
		public const int GradientBrush_ColorBegin = Gdi_ColorBegin;
		public const int GradientBrush_ColorEnd = Gdi_ColorEnd;
		public const int GradientBrush_PointBegin = Gdi_PointBegin;
		public const int GradientBrush_PointEnd = Gdi_PointEnd;

		public const int Pen_Handle = Gdi_Handle + 3;
		public const int Pen_Color = Gdi_Color;
		public const int Pen_Width = 151;
		public const int Pen_DashStyle = 152;
		public const int Pen_LineJoin = 153;		

		public const int Border_Color = Pen_Color;
		public const int Border_Width = Pen_Width;
		public const int Border_Style = Pen_DashStyle;
		public const int Border_Join = Pen_LineJoin;
		public const int Border_Shape = Gdi_Shape;
		public const int Border_Focus = Gdi_Focus;
		public const int Border_Hover = Gdi_Hover;

		public const int Background_Color = Gdi_Color;
		public const int Background_Shape = Gdi_Shape;

		public const int GradientBackground_ColorBegin = GradientBrush_ColorBegin;
		public const int GradientBackground_ColorEnd = GradientBrush_ColorEnd;
		public const int GradientBackground_PointBegin = GradientBrush_PointBegin;
		public const int GradientBackground_PointEnd = GradientBrush_PointEnd;
		public const int GradientBackground_Shape = Gdi_Shape;
		public const int GradientBackground_Direction = 161;

		public const int Font_Handle = Gdi_Handle + 4;
		public const int Text_Color = Gdi_Color;
		public const int Text_Text = 171;
		public const int Text_Family = 172;
		public const int Text_Size = 173;
		public const int Text_Format = 174;

		public const int Line_Handle = Pen_Handle;
		public const int Line_Color = Pen_Color;
		public const int Line_Width = Pen_Width;
		public const int Line_Style = Pen_DashStyle;
		public const int Line_Join = Pen_LineJoin;
		public const int Line_PointBegin = Gdi_PointBegin;
		public const int Line_PointEnd = Gdi_PointEnd;
		public const int Line_PointBeginInternCount = 181;
		public const int Line_PointEndInternCount = 182;
		public const int Line_PointBeginAddress = 10000;
		public const int Line_PointEndAddress = Line_PointBeginAddress + LineX_PointAddressLength;
		public const int LineM_Lazy = Attr_MessageAddress + 100;
		public const int LineM_BuildPoint = Attr_MessageAddress + 101;
		public const int LineX_PointAddressLength = 100;
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
