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
	public class PixelElementRendererBag
	{
		public RectangleF[] Points { set; get; }
		public Color Color { set; get; }
	}

	public class PixelElementRenderer : SolidBrushRenderer<PixelElementRenderer, PixelElement>
	{
		public PixelElementRenderer()
		{
			this[GraphicsDefines.Pixel_Count] = 0;
		}

		protected override void _CreateBrush()
		{
			var pixels = _element[GraphicsDefines.Pixel_Pixels] as IEnumerable<PixelElementRendererBag>;
			if (pixels == null)
			{
				this[GraphicsDefines.Pixel_Count] = 0;
				return;
			}
			int cnt = pixels.Count();
			this[GraphicsDefines.Pixel_Count] = cnt;
			for (int i = 0; i < cnt; i++)
			{
				this[GraphicsDefines.SolidBrush_Handle + i] = new SolidBrush(pixels.ElementAt(i).Color);
			}
		}

		protected override void _DestroyBrush()
		{
			int cnt = (int)this[GraphicsDefines.Pixel_Count];
			for (int i = 0; i < cnt; i++)
			{
				_ReleaseHandle(GraphicsDefines.SolidBrush_Handle + i);				
			}
		}

		protected override void _Render(Rectangle bound)
		{
			var pixels = _element[GraphicsDefines.Pixel_Pixels] as List<PixelElementRendererBag>;
			int cnt = (int)this[GraphicsDefines.Pixel_Count];
			for (int i = 0; i < cnt; i++)
			{
				_graphics.FillRectangles(
					this[GraphicsDefines.SolidBrush_Handle + i] as Brush,
					pixels[i].Points);
			}
		}

		public override void OnElementStateChanged(int state, object value)
		{
			switch (state)
			{
				case GraphicsDefines.Pixel_Refresh:
					_Destroy();
					_Create();
					break;
				default:
					break;
			}
		}
	}
}
