using SimuElectricity.Common.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Interpolation
{
	public enum InterpolationArgsAction
	{
		Reset,
		Execute,
		Refresh
	}

	public class InterpolationArgs
	{
		public InterpolationArgsAction Action { set; get; }
		public Size Slice { set; get; }
		public Size Spacing { set; get; }
		public Color Color { set; get; }
		public Func<double, bool> Estimate { set; get; }
	}
}
