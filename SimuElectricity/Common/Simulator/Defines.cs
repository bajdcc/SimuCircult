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
		public const int NODE_OFFSET_X = 0;
		public const int NODE_OFFSET_Y = 0;
		public const int NODE_WIDTH = 20;
		public const int NODE_HEIGHT = 20;
		public const int WIDTH_COUNT = 60;
		public const int HEIGHT_COUNT = 30;

		public const float LINE_WIDTH = 1.0f;

		public const double TIME_STEP = 1e-2; // 时刻
		public const double MIN_Q = 1e-10; // 最小电荷
		public const double K_ELEC = 1e-4; // 静电力常数
		public const double CELL_CX = 5.0; // 晶格间距X
		public const double CELL_CY = 5.0; // 晶格间距Y
		public const double MAX_TRANSFER_Q = 1e6; // 最大传输正电荷
		public const double MIN_TRANSFER_Q = -1e6; // 最大传输负电荷
		public const double MAX_TRANSFER_I = 1e100; // 最大传输电流
		public const double MIN_TRANSFER_I = -1e100; // 最小传输电流

		public const int NODE_SUBDIVISION_WIDTH = 4;
		public const int NODE_SUBDIVISION_HEIGHT = 4;
		public const int CONTOUR_LINE_COUNT = 60;

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
}
