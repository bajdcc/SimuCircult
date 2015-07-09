using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	abstract class Node : Markable
	{
		private List<Wire> _wires;

		public Node()
		{
			 _wires = new List<Wire>();
		}

		public List<Wire> Wires
		{
			get { return _wires; }
			set { _wires = value; }
		}
	}
}
