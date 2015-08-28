using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
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
	public abstract class Wire<T> : Mutable<T>
		where T : Status, new()
	{
		public Wire()
		{

		}

		private Node<T> _left;

		/// <summary>
		/// 左结点
		/// </summary>
		public Node<T> Left
		{
			get { return _left; }
			set { _left = value; }
		}

		private Node<T> _right;

		/// <summary>
		/// 右结点
		/// </summary>
		public Node<T> Right
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
		/// 是否为单元外部连线（独立绘制）
		/// </summary>
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

		protected abstract void _FromWireToNode(T inputs);

		protected abstract void _FromNodeToWire(T outputs);

		/// <summary>
		/// 激活结点的出边
		/// </summary>
		protected virtual void _ActivateNodeOfWire()
		{
			_right.Activate(ActivateType.FilterNode);
		}
	}
}
