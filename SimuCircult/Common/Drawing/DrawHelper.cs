using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace System.Drawing
{
	public static class DrawHelper
	{
		public static Rectangle AdjustBound(this Rectangle absBound, Rectangle relBound)
		{
			var ext = absBound;
			ext.Offset(relBound.Location);
			ext.Intersect(absBound);
			return ext;
		}
	}
}
