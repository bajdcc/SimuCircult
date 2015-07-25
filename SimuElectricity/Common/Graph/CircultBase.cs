using SimuCircult.Common.Simulator;
using SimuElectricity.Common.Base;
using SimuElectricity.Common.Element;
using SimuElectricity.Common.Interpolation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Graph
{
	public class CircultBase<_NODE_STATUS, _NODE, _WIRE_STATUS, _WIRE, _UNIT_STATUS, _UNIT, _INTERPOLATE>
		where _NODE_STATUS : NodeStatus, new()
		where _NODE : Node<_NODE_STATUS, _WIRE_STATUS>, new()
		where _WIRE_STATUS : WireStatus, new()
		where _WIRE : Wire<_WIRE_STATUS, _NODE_STATUS>, new()
		where _UNIT_STATUS : UnitStatus, new()
		where _UNIT : Unit<_UNIT_STATUS, _NODE_STATUS, _WIRE_STATUS>, new()
		where _INTERPOLATE : InterpolationBase<_NODE_STATUS>, new()
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

		protected _NODE CreateNode()
		{
			var node = new _NODE();
			_nodes.Add(node.Id, node);
			return node;
		}

		protected _UNIT CreateUnit()
		{
			var unit = new _UNIT();
			_units.Add(unit.Id, unit);
			return unit;
		}

		protected void SetUnitInterpolatingMethod(_UNIT unit)
		{
			unit.Interpolating = new _INTERPOLATE();
		}

		protected void ConnectNode(_NODE left, _NODE right, bool external = false)
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

		public virtual void Update()
		{
			_nodes.Values.AsParallel().ForAll(a => a.Advance(AdvanceType.NodeToWire));
			_wires.Values.AsParallel().ForAll(a => a.Advance(AdvanceType.NodeToWire));
			_wires.Values.AsParallel().ForAll(a => a.Update());
			_wires.Values.AsParallel().ForAll(a => a.Advance(AdvanceType.WireToNode));
			_nodes.Values.AsParallel().ForAll(a => a.Advance(AdvanceType.WireToNode));
			_nodes.Values.AsParallel().ForAll(a => a.Update());
		}
	}
}
