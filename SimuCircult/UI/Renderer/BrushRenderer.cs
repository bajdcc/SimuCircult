using SimuCircult.UI.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Element
{
	public abstract class BrushRenderer<T, U> : GraphicsRenderer<T, U>
		where T : GraphicsRenderer<T, U>, new()
		where U : GraphicsElement<U>, new()
	{
		private Brush _brush;

		public Brush Brush
		{
			get { return _brush; }
			set { _brush = value; }
		}

		private Color _color;

		public Color Color
		{
			get { return _color; }
			set { _color = value; }
		}

		abstract protected void CreateBrush(Graphics graphics);

		abstract protected void DestroyBrush(Graphics graphics);

		override protected void _Start()
		{
			_Continue();
		}

		override protected void _Stop()
		{
			_Suspend();
		}

		protected void _Suspend()
		{
			DestroyBrush(_graphics);
		}

		protected void _Continue()
		{
			CreateBrush(_graphics);
		}

		override protected void OnChangedGraphics(Graphics oldGraphics, Graphics newGraphics)
		{
			_Suspend();
			_Continue();
		}
	}
}
