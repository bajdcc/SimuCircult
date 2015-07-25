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
		protected T[,] _pts = new T[2, 2];
		protected List<PixelElementRendererBag> _paint = new List<PixelElementRendererBag>();

		public void SetCoordinate(Point co)
		{
			_co = co;
		}

		public void SetPoints(IEnumerable<T> pts)
		{
			_pts[0, 0] = pts.ElementAt(0);
			_pts[0, 1] = pts.ElementAt(1);
			_pts[1, 0] = pts.ElementAt(2);
			_pts[1, 1] = pts.ElementAt(3);
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
