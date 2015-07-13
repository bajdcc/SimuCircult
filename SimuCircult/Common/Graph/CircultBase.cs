using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Graph
{
	public class CircultBase<_STATUS, _NODE, _WIRE, _UNIT>
		where _STATUS : Status, new()
		where _NODE : Node<_STATUS>, new()
		where _WIRE : Wire<_STATUS>, new()
		where _UNIT : Unit<_STATUS>, new()
	{
		private Dictionary<Guid, _NODE> _nodes = new Dictionary<Guid, _NODE>();

		public Dictionary<Guid, _NODE> Nodes
		{
			get { return _nodes; }
		}

		private Dictionary<Guid, _WIRE> _wires = new Dictionary<Guid, _WIRE>();

		public Dictionary<Guid, _WIRE> Wires
		{
			get { return _wires; }
		}

		private Dictionary<Guid, _UNIT> _units = new Dictionary<Guid, _UNIT>();

		public Dictionary<Guid, _UNIT> Units
		{
			get { return _units; }
		}

		public T CreateNode<T>()
			where T : _NODE, new()
		{
			T node = new T();
			_nodes.Add(node.Id, node);
			return node;
		}

		public T CreateUnit<T>()
			where T : _UNIT, new()
		{
			var unit = new T();
			_units.Add(unit.Id, unit);
			return unit;
		}

		public void ConnectNode<U, V>(U left, V right, bool external = false)
			where U : _NODE, new()
			where V : _NODE, new()
		{
			var wire = new _WIRE();
			_wires.Add(wire.Id, wire);
			wire.Direction = WireType.LeftToRight;
			wire.External = external;
			wire.Left = left;
			wire.Right = right;
			wire.Left.OutWires.Add(wire);
			wire.Right.InWires.Add(wire);
		}

		public void ConnectUnitDirect<U, V>(U left, V right)
			where U : _UNIT, new()
			where V : _UNIT, new()
		{
			var wire = new _WIRE();
			_wires.Add(wire.Id, wire);
			wire.Direction = WireType.LeftToRight;
			wire.External = true;
			wire.Left = left.GetSingleOutput();
			wire.Right = right.GetSingleInput();
			wire.Left.OutWires.Add(wire);
			wire.Right.InWires.Add(wire);
		}

		public void ConnectUnitMoreInput<U, V>(U left, V right, int position)
			where U : _UNIT, new()
			where V : _UNIT, new()
		{
			var wire = new _WIRE();
			_wires.Add(wire.Id, wire);
			wire.Direction = WireType.LeftToRight;
			wire.External = true;
			wire.Left = left.GetSingleOutput();
			wire.Right = right.Inputs[position];
			wire.Left.OutWires.Add(wire);
			wire.Right.InWires.Add(wire);
		}

		public void ConnectUnitMoreOutput<U, V>(U left, V right, int position)
			where U : _UNIT, new()
			where V : _UNIT, new()
		{
			var wire = new _WIRE();
			_wires.Add(wire.Id, wire);
			wire.Direction = WireType.LeftToRight;
			wire.External = true;
			wire.Left = left.Outputs[position];
			wire.Right = right.GetSingleInput();
			wire.Left.OutWires.Add(wire);
			wire.Right.InWires.Add(wire);
		}

		public T ConnectNode<T, U, V>(U left, V right, T wire, WireType type)
			where T : _WIRE, new()
			where U : _NODE, new()
			where V : _NODE, new()
		{
			_wires.Add(wire.Id, wire);
			wire.Direction = type;
			wire.Left = left;
			wire.Right = right;
			switch (type)
			{
				case WireType.LeftToRight:
					left.OutWires.Add(wire);
					right.InWires.Add(wire);
					break;
				case WireType.RightToLeft:
					left.InWires.Add(wire);
					right.OutWires.Add(wire);
					break;
				case WireType.Both:
					left.InWires.Add(wire);
					left.OutWires.Add(wire);
					right.InWires.Add(wire);
					right.OutWires.Add(wire);
					break;
				default:
					break;
			}
			return wire;
		}

		public virtual void Update()
		{
			_units.Values.AsParallel().ForAll(a => a.Activate());
			_nodes.Values.AsParallel().ForAll(a => a.Advance(AdvanceType.NodeToWire));
			_nodes.Values.AsParallel().ForAll(a => a.Update());
			_wires.Values.AsParallel().ForAll(a => a.Advance(AdvanceType.NodeToWire));
			_wires.Values.AsParallel().ForAll(a => a.Update());
			_nodes.Values.AsParallel().ForAll(a => a.Advance(AdvanceType.WireToNode));
			_nodes.Values.AsParallel().ForAll(a => a.Update());
			_wires.Values.AsParallel().ForAll(a => a.Advance(AdvanceType.WireToNode));
			_wires.Values.AsParallel().ForAll(a => a.Update());
		}
	}
}
