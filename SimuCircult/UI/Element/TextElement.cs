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
	public class TextElement : GraphicsElement<TextElement>
	{
		public TextElement()
		{
			this[GraphicsDefines.Text_Color] = Color.Black;
			this[GraphicsDefines.Text_Text] = string.Empty;
			this[GraphicsDefines.Text_Family] = CreateFontFamily();
			this[GraphicsDefines.Text_Size] = 16.0f;
			this[GraphicsDefines.Text_Format] = CreateStringFormat();
		}

		private static FontFamily CreateFontFamily()
		{
			var fontFamily = FontFamily.GenericSansSerif;
			return fontFamily;
		}

		private static StringFormat CreateStringFormat()
		{
			var stringFormat = StringFormat.GenericDefault;
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Center;
			return stringFormat;
		}
	}
}
