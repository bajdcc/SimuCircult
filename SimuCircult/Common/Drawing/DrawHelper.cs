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
		/// <summary>
		/// 调整矩形大小
		/// </summary>
		/// <param name="absBound">绝对位置</param>
		/// <param name="relBound">相对位置</param>
		/// <returns></returns>
		public static Rectangle AdjustBound(this Rectangle absBound, Rectangle relBound)
		{
			absBound.Offset(relBound.Location);
			absBound.Size = relBound.Size;
			absBound.Intersect(absBound);
			return absBound;
		}

		/// <summary>
		/// 设置位置
		/// </summary>
		/// <param name="bound"></param>
		/// <param name="location"></param>
		public static void SetLocation(this Rectangle bound, Point location)
		{
			bound.Location = location;
		}

		/// <summary>
		/// 偏移
		/// </summary>
		/// <param name="bound"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		public static Rectangle OffsetBound(this Rectangle bound, Size offset)
		{
			bound.Location += offset;
			return bound;
		}

		/// <summary>
		/// 缩小
		/// </summary>
		/// <param name="bound"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public static Rectangle Deflate(this Rectangle bound, Size size)
		{
			bound.Inflate(size.Negative());
			return bound;
		}

		/// <summary>
		/// 居中
		/// </summary>
		/// <param name="bound"></param>
		/// <returns></returns>
		public static Point Center(this Rectangle bound)
		{
			return bound.Location + bound.Size.Half();
		}

		/// <summary>
		/// 上/下/左/右居中
		/// </summary>
		/// <param name="bound"></param>
		/// <param name="size"></param>
		/// <param name="type"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 上/下/左/右居中加偏移
		/// </summary>
		/// <param name="bound"></param>
		/// <param name="size"></param>
		/// <param name="type"></param>
		/// <param name="offset"></param>
		/// <returns></returns>
		public static Rectangle NearCenter(this Rectangle bound, Size size, Direction type, Point offset)
		{
			var _bound = bound.NearCenter(size, type);
			_bound.Offset(offset);
			return _bound;
		}

		/// <summary>
		/// 大小减半
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static Size Half(this Size size)
		{
			size.Width /= 2;
			size.Height /= 2;
			return size;
		}

		/// <summary>
		/// 大小取反
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static Size Negative(this Size size)
		{
			size.Width = -size.Width;
			size.Height = -size.Height;
			return size;
		}
	}
}
