using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	abstract class WireX : Markable
	{
		private Node _left;
		private Node _right;

		public Node Left
		{
			get { return _left; }
			set { _left = value; }
		}

		public Node Right
		{
			get { return _right; }
			set { _right = value; }
		}
	}
}
