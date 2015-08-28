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
	/// <summary>
	/// 与结点
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class AndNode<T> : LogicNode<T>
		where T : Status, new()
	{
		public AndNode()
			: base((a, b) => Status._And(a, b))
		{
			
		}

		/// <summary>
		/// 设置初值
		/// </summary>
		/// <param name="seed"></param>
		/// <returns></returns>
		protected override T _InitialSeed(T seed)
		{
			seed.Code = Constants.HIGH_LEVEL;
			return seed;
		}

		protected override void SetString(int code)
		{
			_L3_level[GraphicsDefines.Text_Text] = Constants.AndString;
		}

		public override void Draw(Rectangle bound)
		{
			base.Draw(bound);
		}
	}
}
