﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Simulator
{
	public static class Defines
	{
		public const int NODE_SMALL = 1;
		public const int NODE_OFFSET_X = 0;
		public const int NODE_OFFSET_Y = 20;
		public const int NODE_WIDTH = 30;
		public const int NODE_HEIGHT = 30;
		public const int WIDTH_COUNT = 30;
		public const int HEIGHT_COUNT = 20;

		public const float LINE_WIDTH = 0.1f;

		public const int NODE_SUBDIVISION_WIDTH = 10;
		public const int NODE_SUBDIVISION_HEIGHT = 10;
		public const int CONTOUR_LINE_COUNT = 20;

		public static readonly Random Rand = new Random();
	}
}
