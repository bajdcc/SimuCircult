using SimuCircult.Common.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Graph
{
	class Circult<_NODE, _WIRE>
		where _NODE : Node, new()
		where _WIRE : Wire, new()
	{
		private Dictionary<Guid, _NODE> _nodes = new Dictionary<Guid, _NODE>();
		private Dictionary<Guid, _WIRE> _wires = new Dictionary<Guid, _WIRE>();

		public Node CreateNode()
		{
			_NODE node = new _NODE();
			_nodes.Add(node.Id, node);
			return node;
		}

		public Wire ConnectNode(Guid left, Guid right)
		{
			_WIRE wire = new _WIRE();
			_wires.Add(wire.Id, wire);
			_NODE _left = _nodes[left];
			_NODE _right = _nodes[right];
			wire.Left = _left;
			wire.Right = _right;
			_left.Wires.Add(wire);
			_right.Wires.Add(wire);
			return wire;
		}
	}
}
