using SimuCircult.UI.Drawing;
using SimuCircult.UI.Element;
using SimuCircult.UI.Renderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

		private static Control _ctrl;

		public static Control Ctrl
		{
			get { return Storage._ctrl; }
			set { Storage._ctrl = value; }
		}

		private static ToolTip _tip;

		public static ToolTip Tip
		{
			get { return Storage._tip; }
			set { Storage._tip = value; }
		}

		private static Timer _delay;

		public static Timer Delay
		{
			get { return _delay; }
		}

		public static Point MousePosition
		{
			get { return _ctrl.PointToClient(Control.MousePosition); }
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
			PixelElementRenderer.Register();

			BorderElement.Register();
			BackgroundElement.Register();
			GradientBackgroundElement.Register();
			TextElement.Register();
			LineElement.Register();
			PixelElement.Register();
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
			_graphics.SmoothingMode = SmoothingMode.AntiAlias;
			_graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			_graphics.CompositingQuality = CompositingQuality.HighQuality;
			_delay = new Timer();
		}
	}
}
