using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	public abstract class Node<T> : Mutable<T>
		where T : Status, new()
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

		public override void Advance(AdvanceType type)
		{
			switch (type)
			{
				case AdvanceType.NodeToWire:
					_FromNodeToWire(_outWires.Select(a => a.Next));
					break;
				case AdvanceType.WireToNode:
					_FromWireToNode(_inWires.Select(a => a.Next));
					break;
				default:
					break;
			}
		}

		protected abstract void _FromWireToNode(IEnumerable<T> inputs);

		protected abstract void _FromNodeToWire(IEnumerable<T> outputs);
	}
}
