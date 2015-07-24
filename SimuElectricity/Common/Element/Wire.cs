using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	public enum WireType
	{
		LeftToRight,
		RightToLeft,
		Both,
	}

	public abstract class Wire<T, U> : Mutable<T>
		where T : Status, new()
		where U : Status, new()
	{
		public Wire()
		{

		}

		private Node<U, T> _left;

		public Node<U, T> Left
		{
			get { return _left; }
			set { _left = value; }
		}

		private Node<U, T> _right;

		public Node<U, T> Right
		{
			get { return _right; }
			set { _right = value; }
		}

		private WireType _direction;

		public WireType Direction
		{
			get { return _direction; }
			set { _direction = value; }
		}

		private bool external = false;

		public bool External
		{
			get { return external; }
			set { external = value; }
		}

		private WireType _heading = WireType.Both;

		public WireType Heading
		{
			get { return _heading; }
			set { _heading = value; }
		}

		public override void Activate(ActivateType type)
		{
			switch (type)
			{
				case ActivateType.FilterNode:
					break;
				case ActivateType.FilterWire:
					base.Activate(type);
					break;
				case ActivateType.FilterUnit:
					break;
				default:
					break;
			}
		}

		public override void Advance(AdvanceType type)
		{
			switch (type)
			{
				case AdvanceType.NodeToWire:
					_FromNodeToWire(_left.Next);
					Update();
					break;
				case AdvanceType.WireToNode:
					_FromWireToNode(_right.Next);
					_ActivateNodeOfWire();
					break;
				default:
					break;
			}
		}

		protected abstract void _FromWireToNode(U inputs);

		protected abstract void _FromNodeToWire(U outputs);

		protected virtual void _ActivateNodeOfWire()
		{
			_right.Activate(ActivateType.FilterNode);
		}
	}
}
