using SimuCircult.UI.Drawing;
using SimuCircult.UI.Element;
using SimuCircult.UI.Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Global
{
	public static class Storage
	{
		static private Dictionary<string, IGraphicsRendererFactory> _rendererFactory = new Dictionary<string, IGraphicsRendererFactory>();
		static public Dictionary<string, IGraphicsRendererFactory> RendererFactory
		{
			get { return _rendererFactory; }
			set { _rendererFactory = value; }
		}

		static private Dictionary<string, IGraphicsElementFactory> _elementFactory = new Dictionary<string, IGraphicsElementFactory>();
		static public Dictionary<string, IGraphicsElementFactory> ElementFactory
		{
			get { return Storage._elementFactory; }
			set { Storage._elementFactory = value; }
		}

		static private Graphics _graphics;

		public static Graphics Graphics
		{
			get { return Storage._graphics; }
		}

		static private Bitmap _bitmap;

		public static Bitmap Bitmap
		{
			get { return Storage._bitmap; }
		}

		private static Size _size;

		public static Size Size
		{
			get { return Storage._size; }
			set { Storage._size = value; }
		}

		static Storage()
		{
			RegisterRenderer();			
		}

		private static void RegisterRenderer()
		{
			BorderElementRenderer.Register();
			BackgroundElementRenderer.Register();
			GradientBackgroundElementRenderer.Register();
			TextElementRenderer.Register();
			LineElementRenderer.Register();

			BorderElement.Register();
			BackgroundElement.Register();
			GradientBackgroundElement.Register();
			TextElement.Register();
			LineElement.Register();
		}

		public static void InitializeGui(Size size)
		{
			if (_graphics != null)
			{
				_graphics.Dispose();
				_graphics = null;
				_bitmap.Dispose();
				_bitmap = null;
			}
			_size = size;
			_bitmap = new Bitmap(size.Width, size.Height);
			_graphics = Graphics.FromImage(_bitmap);
		}
	}
}
