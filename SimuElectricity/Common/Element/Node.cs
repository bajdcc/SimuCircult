using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using SimuElectricity.Common.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	public abstract class Node<T, U> : Mutable<T>
		where T : NodeStatus, new()
		where U : WireStatus, new()
	{
		public Node()
		{
			base.Activate(ActivateType.FilterNode);
		}

		private Point _coordinate = Point.Empty;

		public Point Coordinate
		{
			get { return _coordinate; }
			set { _coordinate = value; }
		}

		private List<Wire<U, T>> _inWires = new List<Wire<U, T>>();

		public List<Wire<U, T>> InWires
		{
			get { return _inWires; }
			set { _inWires = value; }
		}

		private List<Wire<U, T>> _outWires = new List<Wire<U, T>>();

		public List<Wire<U, T>> OutWires
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
					Update();
					break;
				default:
					break;
			}			
		}

		protected abstract void _FromWireToNode(IEnumerable<U> inputs);

		protected abstract void _FromNodeToWire(IEnumerable<U> outputs);
	}
}
