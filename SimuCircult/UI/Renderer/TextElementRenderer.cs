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
	public class TextElementRenderer : SolidBrushRenderer<TextElementRenderer, TextElement>
	{
		protected void _CreateFont()
		{
			this[GraphicsDefines.Font_Handle] = new Font(
				_element[GraphicsDefines.Text_Family] as FontFamily,
				(float)_element[GraphicsDefines.Text_Size]);
		}

		protected void _DestroyFont()
		{
			_ReleaseHandle(GraphicsDefines.Font_Handle);
		}

		protected override void CreateGdiObject(Graphics graphics)
		{
			base.CreateGdiObject(graphics);
			_CreateFont();
		}

		protected override void DestroyGdiObject(Graphics graphics)
		{
			base.DestroyGdiObject(graphics);
			_DestroyFont();
		}

		public override void Render(Rectangle bound)
		{
			
		}

		public override void OnElementStateChanged(int state, object value)
		{
			switch (state)
			{
				case GraphicsDefines.Text_Color:
					base.OnElementStateChanged(state, value);
					break;
				case GraphicsDefines.Text_Text:
				case GraphicsDefines.Text_Family:
				case GraphicsDefines.Text_Size:
					_DestroyFont();
					_CreateFont();
					break;
				default:
					break;
			}
		}
	}
}
