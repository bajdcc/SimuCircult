using SimuCircult.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	public enum UnitType
	{
		None,
		Switch,
	}

	class Unit<T> : Markable, Mutable<T>
	{
		private UnitType _unitType = UnitType.None;

		public UnitType UnitType
		{
			get { return _unitType; }
		}

		public void Activate()
		{
			if (_unitType == Element.UnitType.Switch)
			{

			}
		}
	}
}
