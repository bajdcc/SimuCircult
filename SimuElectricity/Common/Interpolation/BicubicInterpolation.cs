using SimuCircult.UI.Renderer;
using SimuElectricity.Common.Base;
using SimuElectricity.Common.Simulator;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuElectricity.Common.Interpolation
{
	/// <summary>
	/// 双立方插值
	/// </summary>
	public class BicubicInterpolation<T> : InterpolationBase<T>
		where T : NodeStatus, new()
	{
		private double[,] _coef = new double[4, 4];

		public BicubicInterpolation() : base(4, 4)
		{

		}

		public override void Interpolate(InterpolationArgs args)
		{
			ComputeCoefficient();
			TestPixel(args);
		}

		/// <summary>
		/// 插值方程
		/// </summary>
		private void ComputeCoefficient()
		{
			_coef[0, 0] = Pts[1, 1].NQ;
			_coef[0, 1] = -.5 * Pts[1, 0].NQ + .5 * Pts[1, 2].NQ;
			_coef[0, 2] = Pts[1, 0].NQ - 2.5 * Pts[1, 1].NQ + 2 * Pts[1, 2].NQ - .5 * Pts[1, 3].NQ;
			_coef[0, 3] = -.5 * Pts[1, 0].NQ + 1.5 * Pts[1, 1].NQ - 1.5 * Pts[1, 2].NQ + .5 * Pts[1, 3].NQ;
			_coef[1, 0] = -.5 * Pts[0, 1].NQ + .5 * Pts[2, 1].NQ;
			_coef[1, 1] = .25 * Pts[0, 0].NQ - .25 * Pts[0, 2].NQ - .25 * Pts[2, 0].NQ + .25 * Pts[2, 2].NQ;
			_coef[1, 2] = -.5 * Pts[0, 0].NQ + 1.25 * Pts[0, 1].NQ - Pts[0, 2].NQ + .25 * Pts[0, 3].NQ
				+ .5 * Pts[2, 0].NQ - 1.25 * Pts[2, 1].NQ + Pts[2, 2].NQ - .25 * Pts[2, 3].NQ;
			_coef[1, 3] = .25 * Pts[0, 0].NQ - .75 * Pts[0, 1].NQ + .75 * Pts[0, 2].NQ - .25 * Pts[0, 3].NQ
				- .25 * Pts[2, 0].NQ + .75 * Pts[2, 1].NQ - .75 * Pts[2, 2].NQ + .25 * Pts[2, 3].NQ;
			_coef[2, 0] = Pts[0, 1].NQ - 2.5 * Pts[1, 1].NQ + 2 * Pts[2, 1].NQ - .5 * Pts[3, 1].NQ;
			_coef[2, 1] = -.5 * Pts[0, 0].NQ + .5 * Pts[0, 2].NQ + 1.25 * Pts[1, 0].NQ - 1.25 * Pts[1, 2].NQ
				- Pts[2, 0].NQ + Pts[2, 2].NQ + .25 * Pts[3, 0].NQ - .25 * Pts[3, 2].NQ;
			_coef[2, 2] = Pts[0, 0].NQ - 2.5 * Pts[0, 1].NQ + 2 * Pts[0, 2].NQ - .5 * Pts[0, 3].NQ
				- 2.5 * Pts[1, 0].NQ + 6.25 * Pts[1, 1].NQ - 5 * Pts[1, 2].NQ + 1.25 * Pts[1, 3].NQ
				+ 2 * Pts[2, 0].NQ - 5 * Pts[2, 1].NQ + 4 * Pts[2, 2].NQ - Pts[2, 3].NQ - .5 * Pts[3, 0].NQ
				+ 1.25 * Pts[3, 1].NQ - Pts[3, 2].NQ + .25 * Pts[3, 3].NQ;
			_coef[2, 3] = -.5 * Pts[0, 0].NQ + 1.5 * Pts[0, 1].NQ - 1.5 * Pts[0, 2].NQ + .5 * Pts[0, 3].NQ
				+ 1.25 * Pts[1, 0].NQ - 3.75 * Pts[1, 1].NQ + 3.75 * Pts[1, 2].NQ - 1.25 * Pts[1, 3].NQ
				- Pts[2, 0].NQ + 3 * Pts[2, 1].NQ - 3 * Pts[2, 2].NQ + Pts[2, 3].NQ + .25 * Pts[3, 0].NQ
				- .75 * Pts[3, 1].NQ + .75 * Pts[3, 2].NQ - .25 * Pts[3, 3].NQ;
			_coef[3, 0] = -.5 * Pts[0, 1].NQ + 1.5 * Pts[1, 1].NQ - 1.5 * Pts[2, 1].NQ + .5 * Pts[3, 1].NQ;
			_coef[3, 1] = .25 * Pts[0, 0].NQ - .25 * Pts[0, 2].NQ - .75 * Pts[1, 0].NQ + .75 * Pts[1, 2].NQ
				+ .75 * Pts[2, 0].NQ - .75 * Pts[2, 2].NQ - .25 * Pts[3, 0].NQ + .25 * Pts[3, 2].NQ;
			_coef[3, 2] = -.5 * Pts[0, 0].NQ + 1.25 * Pts[0, 1].NQ - Pts[0, 2].NQ + .25 * Pts[0, 3].NQ
				+ 1.5 * Pts[1, 0].NQ - 3.75 * Pts[1, 1].NQ + 3 * Pts[1, 2].NQ - .75 * Pts[1, 3].NQ
				- 1.5 * Pts[2, 0].NQ + 3.75 * Pts[2, 1].NQ - 3 * Pts[2, 2].NQ + .75 * Pts[2, 3].NQ
				+ .5 * Pts[3, 0].NQ - 1.25 * Pts[3, 1].NQ + Pts[3, 2].NQ - .25 * Pts[3, 3].NQ;
			_coef[3, 3] = .25 * Pts[0, 0].NQ - .75 * Pts[0, 1].NQ + .75 * Pts[0, 2].NQ - .25 * Pts[0, 3].NQ
				- .75 * Pts[1, 0].NQ + 2.25 * Pts[1, 1].NQ - 2.25 * Pts[1, 2].NQ + .75 * Pts[1, 3].NQ
				+ .75 * Pts[2, 0].NQ - 2.25 * Pts[2, 1].NQ + 2.25 * Pts[2, 2].NQ - .75 * Pts[2, 3].NQ
				- .25 * Pts[3, 0].NQ + .75 * Pts[3, 1].NQ - .75 * Pts[3, 2].NQ + .25 * Pts[3, 3].NQ;
		}

		private double ComputeValue(double x, double y)
		{
			var x2 = x * x;
			var x3 = x2 * x;
			var y2 = y * y;
			var y3 = y2 * y;
			return (_coef[0, 0] + _coef[0, 1] * y + _coef[0, 2] * y2 + _coef[0, 3] * y3) +
				   (_coef[1, 0] + _coef[1, 1] * y + _coef[1, 2] * y2 + _coef[1, 3] * y3) * x +
				   (_coef[2, 0] + _coef[2, 1] * y + _coef[2, 2] * y2 + _coef[2, 3] * y3) * x2 +
				   (_coef[3, 0] + _coef[3, 1] * y + _coef[3, 2] * y2 + _coef[3, 3] * y3) * x3;  
		}

		private void TestPixel(InterpolationArgs args)
		{
			var slice = new SizeF(
				1.0f * args.Spacing.Width / args.Slice.Width,
				1.0f * args.Spacing.Height / args.Slice.Height);		
			var rts = new ConcurrentBag<RectangleF>();
			Parallel.For(0, args.Slice.Width * args.Slice.Height, n =>
			{
				var i = 1.0f * (n / args.Slice.Width) / args.Slice.Width;
				var j = 1.0f * (n % args.Slice.Width) / args.Slice.Height;
				var pt = new PointF(0.5f / args.Slice.Width + i, 0.5f / args.Slice.Height + j);
				var val = ComputeValue(pt.X, pt.Y);
				if (args.Estimate(val))
				{
					rts.Add(new RectangleF(
						new PointF(
							_co.X + i * args.Spacing.Width,
							_co.Y + j * args.Spacing.Height
						),
						new SizeF(
							slice.Width + 0.5f,
							slice.Height + 0.5f
					)));
				}
			});
			if (rts.Count > 0)
			{
				_paint.Add(new PixelElementRendererBag() { Color = args.Color, Points = rts.ToArray() });
			}
		}
	}
}
