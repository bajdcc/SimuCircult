using SimuCircult.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	abstract class Node<T> : Markable, Mutable<T>
	{
		private List<Wire<T>> _inWires = new List<Wire<T>>();

		public List<Wire<T>> InWires
		{
			get { return _inWires; }
			set { _inWires = value; }
		}

		private List<Wire<T>> _outWires = new List<Wire<T>>();

		public List<Wire<T>> OutWires
		{
			get { return _outWires; }
			set { _outWires = value; }
		}
	}
}
