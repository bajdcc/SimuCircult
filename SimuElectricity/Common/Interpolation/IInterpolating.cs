using SimuCircult.UI.Renderer;
using SimuElectricity.Common.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Interpolation
{
	public interface IInterpolating<T>
		where T : NodeStatus, new()
	{
		void SetCoordinate(Point pts);
		void SetPoints(IEnumerable<T> pts);
		IEnumerable<PixelElementRendererBag> GetPixel();
		void Reset();
		void Interpolate(InterpolationArgs args);
	}
}
