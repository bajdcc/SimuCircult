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
			absBound.Offset(relBound.Location);
			absBound.Size = relBound.Size;
			absBound.Intersect(absBound);
			return absBound;
		}

		public static void SetLocation(this Rectangle bound, Point location)
		{
			bound.Location = location;
		}

		public static Rectangle OffsetBound(this Rectangle bound, Size offset)
		{
			bound.Location += offset;
			return bound;
		}

		public static Point Center(this Rectangle bound)
		{
			return bound.Location + bound.Size.Half();
		}

		public static Size Half(this Size size)
		{
			return new Size(size.Width / 2, size.Height / 2);
		}
	}
}
