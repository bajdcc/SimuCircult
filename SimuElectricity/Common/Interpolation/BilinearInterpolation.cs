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
	/// 双线性插值
	/// </summary>
	public class BilinearInterpolation<T> : InterpolationBase<T>
		where T : NodeStatus, new()
	{
		private double[] _coef = new double[4];

		public BilinearInterpolation() : base(2, 2)
		{

		}

		public override void Interpolate(InterpolationArgs args)
		{
			ComputeCoefficient();
			TestPixel(args);
		}

		/// <summary>
		/// 插值方程：
		/// Z=a0+a1*x+a2*y+a3*x*y
		/// a0=n00,a1=n10-n00,a2=n01-n00,a3=n11-n10-n01-n00
		/// </summary>
		private void ComputeCoefficient()
		{
			_coef[0] = Pts[0, 0].Q;
			_coef[1] = Pts[1, 0].Q - Pts[0, 0].Q;
			_coef[2] = Pts[0, 1].Q - Pts[0, 0].Q;
			_coef[3] = Pts[1, 1].Q - _coef[2];
		}

		private double ComputeValue(double x, double y)
		{
			return _coef[0] + _coef[1] * x + _coef[2] * y + _coef[3] * x * y;
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
