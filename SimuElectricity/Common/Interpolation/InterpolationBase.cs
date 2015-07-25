using SimuCircult.UI.Renderer;
using SimuElectricity.Common.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Interpolation
{
	public class InterpolationBase<T> : IInterpolating<T>
		where T : NodeStatus, new()
	{
		protected Point _co = Point.Empty;
		protected Size _size;
		private T[,] _pts;

		protected List<PixelElementRendererBag> _paint = new List<PixelElementRendererBag>();

		public InterpolationBase(int cx, int cy)
		{
			_size = new Size(cx, cy);
			_pts = new T[cx, cy];
		}

		public T[,] Pts
		{
			get { return _pts; }
			set { _pts = value; }
		}

		public void SetCoordinate(Point co)
		{
			_co = co;
		}

		public virtual void SetPoints(IEnumerable<T> pts)
		{
			for (int i = 0; i < _size.Width * _size.Height; i++)
			{
				Pts[i / _size.Width, i % _size.Width] = pts.ElementAt(i);
			}
		}

		public IEnumerable<PixelElementRendererBag> GetPixel()
		{
			return _paint;
		}

		public void Reset()
		{
			_paint.Clear();
		}

		public virtual void Interpolate(InterpolationArgs args)
		{

		}
	}
}
