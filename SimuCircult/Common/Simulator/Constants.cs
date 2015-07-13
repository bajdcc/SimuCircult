using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Simulator
{
	public static class Constants
	{
		public const int LOW_LEVEL = 0;
		public const int HIGH_LEVEL = 1;
	}

	public enum AdvanceType
	{
		NodeToWire,
		WireToNode,
	}
}
