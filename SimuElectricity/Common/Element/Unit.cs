using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using SimuElectricity.Common.Base;
using SimuElectricity.Common.Interpolation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	public abstract class Unit<T, U, V> : Mutable<T>
		where T : UnitStatus, new()
		where U : NodeStatus, new()
		where V : WireStatus, new()
	{
		public Unit()
		{
			base.Activate(ActivateType.FilterUnit);
		}

		private Point _coordinate = Point.Empty;

		public Point Coordinate
		{
			get { return _coordinate; }
			set { _coordinate = value; }
		}

		private List<Node<U, V>> _nodes = new List<Node<U, V>>();

		public List<Node<U, V>> Nodes
		{
			get { return _nodes; }
			set { _nodes = value; }
		}

		private IInterpolating<U> _interpolating;

		internal IInterpolating<U> Interpolating
		{
			get { return _interpolating; }
			set { _interpolating = value; SetInterpolatingInfo(); }
		}

		protected virtual void SetInterpolatingInfo()
		{
			_interpolating.SetCoordinate(_coordinate);
			_interpolating.SetPoints(_nodes.Select(a => a.Local));
		}
	}
}
