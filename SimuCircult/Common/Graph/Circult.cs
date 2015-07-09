using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuCircult.Common.Graph
{
	class Circult<_STATUS, _NODE, _WIRE, _UNIT>
		where _STATUS : Status, new()
		where _NODE : Node<_STATUS>, new()
		where _WIRE : Wire<_STATUS>, new()
		where _UNIT : Unit<_STATUS>, new()
	{
		private Dictionary<Guid, _NODE> _nodes = new Dictionary<Guid, _NODE>();
		private Dictionary<Guid, _WIRE> _wires = new Dictionary<Guid, _WIRE>();
		private Dictionary<Guid, Mutable<_STATUS>> _nodeStatus = new Dictionary<Guid, Mutable<_STATUS>>();
		private Dictionary<Guid, Mutable<_STATUS>> _wireStatus = new Dictionary<Guid, Mutable<_STATUS>>();
		private Dictionary<Guid, _UNIT> _units = new Dictionary<Guid, _UNIT>();

		public _NODE CreateNode()
		{
			_NODE node = new _NODE();
			_nodes.Add(node.Id, node);
			return node;
		}

		public _WIRE ConnectNode(Guid left, Guid right, WireType type)
		{
			_WIRE wire = new _WIRE();
			_wires.Add(wire.Id, wire);
			_NODE _left = _nodes[left];
			_NODE _right = _nodes[right];
			wire.Left = _left;
			wire.Right = _right;
			switch (type)
			{
				case WireType.LeftToRight:
					_left.OutWires.Add(wire);
					_right.InWires.Add(wire);
					break;
				case WireType.RightToLeft:
					_left.InWires.Add(wire);
					_right.OutWires.Add(wire);
					break;
				case WireType.Both:
					_left.InWires.Add(wire);
					_left.OutWires.Add(wire);
					_right.InWires.Add(wire);
					_right.OutWires.Add(wire);
					break;
				default:
					break;
			}
			return wire;
		}

		public void Update()
		{
			Parallel.ForEach(_nodeStatus.Values, a => a.Update());
			Parallel.ForEach(_wireStatus.Values, a => a.Update());
			.AsParallel().Where(a => a.Active)
		}
	}
}
