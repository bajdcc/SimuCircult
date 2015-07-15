using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
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
		public override void Draw(Rectangle bound)
		{

		}
	}
}
