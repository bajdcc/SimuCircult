using SimuCircult.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	abstract class Wire<T> : Markable, Mutable<T>
		where T : Status
	{
		private Node<T> _left;
		private Node<T> _right;

		public Node<T> Left
		{
			get { return _left; }
			set { _left = value; }
		}

		public Node<T> Right
		{
			get { return _right; }
			set { _right = value; }
		}
	}
}
