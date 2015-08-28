using SimuCircult.UI.Renderer;
using SimuElectricity.Common.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Interpolation
{
	/// <summary>
	/// 插值接口
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IInterpolating<T>
		where T : NodeStatus, new()
	{
		/// <summary>
		/// 设置初始坐标
		/// </summary>
		/// <param name="pts"></param>
		void SetCoordinate(Point pts);
		/// <summary>
		/// 设置样本数据
		/// </summary>
		/// <param name="pts"></param>
		void SetPoints(IEnumerable<T> pts);
		/// <summary>
		/// 返回可绘制结构
		/// </summary>
		/// <returns></returns>
		IEnumerable<PixelElementRendererBag> GetPixel();
		/// <summary>
		/// 重置
		/// </summary>
		void Reset();
		/// <summary>
		/// 插值
		/// </summary>
		/// <param name="args"></param>
		void Interpolate(InterpolationArgs args);
	}
}
