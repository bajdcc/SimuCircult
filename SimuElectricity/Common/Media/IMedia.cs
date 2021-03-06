﻿using SimuElectricity.Common.Base;
using SimuElectricity.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuElectricity.Common.Media
{
	/// <summary>
	/// 介质接口
	/// </summary>
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
		/// <param name="elecStatus">结点电特性</param>
		/// <param name="status">结点间电离状态</param>
		/// <param name="voltage">电位差</param>
		/// <param name="current">产生的电流</param>
		/// <returns>返回电特性</returns>
		ElectricStatus BreakDownTest(IMedia media, ElectricStatus elecStatus, WireStatus status, double voltage, out double current);

		/// <summary>
		/// 进行一次检查，如大地电荷流失等
		/// </summary>
		void Advance();

		/// <summary>
		/// 计算该介质的电场中用到的比例系数
		/// </summary>
		/// <returns></returns>
		double? CalculateElectricField();

        /// <summary>
		/// 计算有效电荷
		/// </summary>
		/// <returns></returns>
		double EffectiveQ();
    }
}
