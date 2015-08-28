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
		/// <summary>
		/// 插值方法（双线性或双立方）
		/// </summary>
		public InterpolationArgsAction Action { set; get; }
		/// <summary>
		/// 分片数
		/// </summary>
		public Size Slice { set; get; }
		/// <summary>
		/// 区域大小
		/// </summary>
		public Size Spacing { set; get; }
		/// <summary>
		/// 颜色
		/// </summary>
		public Color Color { set; get; }
		/// <summary>
		/// 分层筛选渲染函数
		/// </summary>
		public Func<double, bool> Estimate { set; get; }
	}
}
