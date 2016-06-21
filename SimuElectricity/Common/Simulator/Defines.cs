using SimuElectricity.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Simulator
{
	public static class Defines
	{
		public const int NODE_SMALL = 1;
		public const int NODE_OFFSET_X = -10;
		public const int NODE_OFFSET_Y = 0;
		public const int NODE_WIDTH = 15;
		public const int NODE_HEIGHT = 15;
		public const int WIDTH_COUNT = 60;
		public const int HEIGHT_COUNT = 40;

		public const float LINE_WIDTH = 1.0f;

		public const double TIME_STEP = 1; // 时刻
		public const double MIN_Q = 1e-5; // 最小电荷
		public const double MAX_Q = 1e5; // 最大电荷
		public const double K_ELEC = 1; // 静电力常数
		public const double CELL_CX = 5.0; // 晶格间距X
		public const double CELL_CY = 5.0; // 晶格间距Y
		public const double MAX_TRANSFER_Q = 1e5; // 最大传输正电荷
		public const double MIN_TRANSFER_Q = -1e5; // 最大传输负电荷
		public const double MAX_TRANSFER_I = 1e5; // 最大传输正电流
		public const double MIN_TRANSFER_I = -1e5; // 最大传输负电流

		public const int NODE_SUBDIVISION_WIDTH = 2;
		public const int NODE_SUBDIVISION_HEIGHT = 2;
		public const int CONTOUR_LINE_COUNT = 20;

		public static readonly Random Rand = new Random();
		public static readonly NormalDistributionRandom NRand = new NormalDistributionRandom();

		static public double Clamp(double t, double min, double max)
		{
			if (t > max)
			{
				return max;
			}
			else if (t < min)
			{
				return min;
			}
			return t;
		}

		static public double Clamp(double t, double area)
		{
			return Math.Abs(t) < area ? t : Math.Sign(t) * area;
		}
	}

	public enum ElectricStatus
	{
		/// <summary>
		/// 电阻
		/// </summary>
		Resistence,
		/// <summary>
		/// 电离
		/// </summary>
		Ionization,
		/// <summary>
		/// 导电
		/// </summary>
		Conduction,
	}
}
