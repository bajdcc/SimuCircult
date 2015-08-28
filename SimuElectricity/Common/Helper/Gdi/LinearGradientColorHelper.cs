using SimuElectricity.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Helper
{
	/// <summary>
	/// 取渐变颜色
	/// </summary>
	public static class LinearGradientColorHelper
	{
		private static Bitmap _bitmap;//渐变渲染位图
		private static Color[] _colors;//渐变颜色缓存

		static LinearGradientColorHelper()
		{
			var length = Defines.CONTOUR_LINE_COUNT + 2;
			_bitmap = new Bitmap(length, 1);
			var _graphics = Graphics.FromImage(_bitmap);
			//_graphics.SmoothingMode = SmoothingMode.AntiAlias;
			//_graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			//_graphics.CompositingQuality = CompositingQuality.HighQuality;
			var br = new LinearGradientBrush(new Rectangle(0, 0, length, 1), Color.Black, Color.Black, 0, false);			
			var cb = new ColorBlend();//七色渐变
			cb.Positions = new[] { 0, 1 / 6f, 2 / 6f, 3 / 6f, 4 / 6f, 5 / 6f, 1 };
			cb.Colors = new[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };
			br.InterpolationColors = cb;
			_graphics.FillRectangle(br, br.Rectangle);
			_colors = new Color[length];
			for (int i = 0; i < length; i++)
			{
				_colors[i] = _bitmap.GetPixel(i, 0);
			}
		}

		public static Color GetColor(int index)
		{
			return _colors[index];
		}
	}
}
