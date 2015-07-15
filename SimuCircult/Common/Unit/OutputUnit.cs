using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
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
	public class OutputUnit<T> : CommonUnit<T>
		where T : Status, new()
	{
		public OutputUnit()
		{
			_L1_text[GraphicsDefines.Text_Text] = Constants.OutputUnitString;
		}

		public override void Draw(Rectangle bound)
		{
			base.Draw(bound);
		}
	}
}
