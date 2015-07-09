using SimuCircult.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	abstract class Node<T> : Markable, Mutable<T>
		where T : Status
	{
		private List<Wire<T>> _wires;

		public Node()
		{
			_wires = new List<Wire<T>>();
		}

		public List<Wire<T>> Wires
		{
			get { return _wires; }
			set { _wires = value; }
		}
	}
}
