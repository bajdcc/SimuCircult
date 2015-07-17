using SimuCircult.Common.Simulator;
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

		public static Rectangle Deflate(this Rectangle bound, Size size)
		{
			bound.Inflate(size.Negative());
			return bound;
		}

		public static Point Center(this Rectangle bound)
		{
			return bound.Location + bound.Size.Half();
		}

		public static Rectangle NearCenter(this Rectangle bound, Size size, Direction type)
		{
			var _bound = bound;
			switch (type)
			{
				case Direction.Left:
					_bound.Inflate(0, (size.Height - bound.Height) / 2);
					_bound.Width = size.Width;
					break;
				case Direction.Up:
					_bound.Inflate((size.Width - bound.Width) / 2, 0);
					_bound.Height = size.Height;
					break;
				case Direction.Right:
					_bound.Inflate(0, (size.Height - bound.Height) / 2);
					_bound.X = bound.Right - size.Width;
					_bound.Width = size.Width;
					break;
				case Direction.Bottom:
					_bound.Inflate((size.Width - bound.Width) / 2, 0);
					_bound.Y = bound.Bottom - size.Height;
					_bound.Height = size.Height;
					break;
				default:
					break;
			}
			_bound.Intersect(bound);
			return _bound;
		}

		public static Rectangle NearCenter(this Rectangle bound, Size size, Direction type, Point offset)
		{
			var _bound = bound.NearCenter(size, type);
			_bound.Offset(offset);
			return _bound;
		}

		public static Size Half(this Size size)
		{
			size.Width /= 2;
			size.Height /= 2;
			return size;
		}

		public static Size Negative(this Size size)
		{
			size.Width = -size.Width;
			size.Height = -size.Height;
			return size;
		}
	}
}
