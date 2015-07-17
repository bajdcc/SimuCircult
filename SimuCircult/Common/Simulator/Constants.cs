using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Simulator
{
	public static class Constants
	{
		public const int LOW_LEVEL = 0;
		public const int HIGH_LEVEL = 1;

		public static readonly Color WindowBackground = Color.WhiteSmoke;

		public static readonly Color ActiveHighLevel = Color.Blue;
		public static readonly Color ActiveLowLevel = Color.Red;
		public static readonly Color InactiveHighLevel = Color.DarkBlue;
		public static readonly Color InactiveLowLevel = Color.DarkRed;

		public static readonly Color FocusBorder = Color.Gray;
		public static readonly Color HoverBorder = Color.DarkGray;
		public static readonly Color NormalBorder = Color.LightGray;

		public const string LowString = "L";
		public const string HighString = "H";

		public const string AndString = "与";
		public const string OrString = "或";
		public const string NotString = "非";

		public const string AndUnitString = "与门";
		public const string OrUnitString = "或门";
		public const string AndNotUnitString = "与非门";
		public const string OrNotUnitString = "或非门";
		public const string NotUnitString = "非门";
		public const string SwitchUnitString = "开关";
		public const string OutputUnitString = "输出";

		public const string DisplayString = "Circult Information";

		public const string SRLockUnitString = "RS触发器";

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

	public enum Direction
	{
		Left,
		Up,
		Right,
		Bottom,
	}

	public enum HandleType
	{
		Test,
		Down,
		Up,
		Enter,
		Hover,
		Leave,
		Move,
		Focus,
		LostFocus,
		Drag,
	}
}
