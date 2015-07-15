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

namespace SimuCircult.Common.Node
{
	public class OrNode<T> : LogicNode<T>
		where T : Status, new()
	{
		public OrNode()
			: base((a, b) => Status._Or(a, b))
		{
			
		}

		protected override T _InitialSeed(T seed)
		{
			seed.Code = Constants.LOW_LEVEL;
			return seed;
		}

		protected override void SetString(int code)
		{
			_L3_level[GraphicsDefines.Text_Text] = Constants.OrString;
		}

		public override void Draw(Rectangle bound)
		{
			base.Draw(bound);
		}
	}
}
