﻿using SimuCircult.Common.Base;
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
	public class NotUnit<T> : CommonUnit<T>
		where T : Status, new()
	{
		public NotUnit()
		{
			_L1_title[GraphicsDefines.Text_Text] = Constants.NotUnitString;
		}

		public override void Draw(Rectangle bound)
		{
			base.Draw(bound);
		}
	}
}
