using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Element;
using SimuCircult.Common.Node;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Unit
{
	public class ClockUnit<T> : CommonUnit<T>
		where T : Status, new()
	{
		public ClockUnit()
		{
			_L1_title[GraphicsDefines.Text_Text] = Constants.ClockUnitString;
		}

		private GenNode<T> _gen;

		public GenNode<T> Gen
		{
			get { return _gen; }
			set { _gen = value; }
		}

		private const int MAX_ROUND = 30;
		private int _power = Constants.LOW_LEVEL;
		private int _cnt = 0;

		public int Power
		{
			get { return _power; }
		}

		public override void Activate(ActivateType type)
		{
			base.Activate(type);
			if (_cnt++ > MAX_ROUND)
			{
				_cnt = 0;
				_power = Constants.Inverse(_power);
			}			
			if (_gen.Next.Code != _power)
			{
				_gen.Next.Code = _power;
				_gen.Activate(ActivateType.FilterNode);
			}
		}

		protected override bool _Active()
		{
			return true;
		}

		public override void Draw(Rectangle bound)
		{
			base.Draw(bound);
		}
	}
}
