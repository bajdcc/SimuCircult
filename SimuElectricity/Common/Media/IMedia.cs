using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuElectricity.Common.Media
{
	public interface IMedia
	{
		/// <summary>
		/// 获取介质的ID
		/// </summary>
		/// <returns>ID</returns>
		int GetId();

		/// <summary>
		/// 介质间电压与电流关系
		/// </summary>
		/// <param name="media">目标介质</param>
		/// <param name="breaknode">结点是否已经击穿</param>
		/// <param name="breakdown">结点间是否已经击穿</param>
		/// <param name="voltage">电位差</param>
		/// <param name="current">产生的电流</param>
		/// <returns>如果击穿则返回真</returns>
		bool BreakDownTest(IMedia media, bool breaknode, bool breakdown, double voltage, out double current);

		/// <summary>
		/// 进行一次检查，如大地电荷流失等
		/// </summary>
		void Advance();

		/// <summary>
		/// 计算该介质的电场中用到的比例系数
		/// </summary>
		/// <returns></returns>
		double? CalculateElectricField();
	}
}
