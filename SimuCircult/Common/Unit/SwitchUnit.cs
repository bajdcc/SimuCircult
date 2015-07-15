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
	public class SwitchUnit<T> : CommonUnit<T>
		where T : Status, new()
	{
		public SwitchUnit()
		{
			_L1_text[GraphicsDefines.Text_Text] = Constants.SwitchUnitString;
		}

		private GenNode<T> _gen;

		public GenNode<T> Gen
		{
			get { return _gen; }
			set { _gen = value; }
		}

		private int _power = 0;

		public int Power
		{
			get { return _power; }
			set { _power = value; }
		}

		public override void Activate(ActivateType type)
		{
			base.Activate(type);
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
