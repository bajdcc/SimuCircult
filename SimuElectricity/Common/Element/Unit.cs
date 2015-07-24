using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	public abstract class Unit<T, U, V> : Mutable<T>
		where T : Status, new()
		where U : Status, new()
		where V : Status, new()
	{
		public Unit()
		{
			base.Activate(ActivateType.FilterUnit);
		}

		private List<Node<U, V>> _nodes = new List<Node<U, V>>();

		public List<Node<U, V>> Nodes
		{
			get { return _nodes; }
			set { _nodes = value; }
		}
	}
}
