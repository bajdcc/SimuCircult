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

		public static int Inverse(int code)
		{
			return code == LOW_LEVEL ? HIGH_LEVEL : LOW_LEVEL;
		}
	}

	public enum AdvanceType
	{
		NodeToWire,
		WireToNode,
	}

	public enum ActivateType
	{
		FilterNode,
		FilterWire,
		FilterUnit
	}
}
