using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using SimuElectricity.Common.Base;
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

	/// <summary>
	/// 连线基类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="U"></typeparam>
	public abstract class Wire<T, U> : Mutable<T>
		where T : WireStatus, new()
		where U : NodeStatus, new()
	{
		public Wire()
		{

		}

		private Node<U, T> _left;

		/// <summary>
		/// 左结点（起始）
		/// </summary>
		public Node<U, T> Left
		{
			get { return _left; }
			set { _left = value; }
		}

		private Node<U, T> _right;

		/// <summary>
		/// 右结点（结束）
		/// </summary>
		public Node<U, T> Right
		{
			get { return _right; }
			set { _right = value; }
		}

		private WireType _direction;

		/// <summary>
		/// 方向
		/// </summary>
		public WireType Direction
		{
			get { return _direction; }
			set { _direction = value; }
		}

		private bool external = false;

		/// <summary>
		/// 是否需要绘制
		/// </summary>
		public bool External
		{
			get { return external; }
			set { external = value; }
		}

		private WireType _heading = WireType.Both;

		/// <summary>
		/// 电荷传导方向
		/// </summary>
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
					break;
				case AdvanceType.WireToNode:
					_FromWireToNode(_right.Next);
					break;
				default:
					break;
			}
		}

		protected abstract void _FromWireToNode(U inputs);

		protected abstract void _FromNodeToWire(U outputs);
	}
}
