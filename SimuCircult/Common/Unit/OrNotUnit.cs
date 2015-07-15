using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Unit
{
	public class OrNotUnit<T> : CommonUnit<T>
		where T : Status, new()
	{
		public OrNotUnit()
		{
			_L1_text[GraphicsDefines.Text_Text] = Constants.OrNotUnitString;
		}

		public override void Draw(Rectangle bound)
		{
			base.Draw(bound);
		}
	}
}
