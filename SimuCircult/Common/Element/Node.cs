using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	/// <summary>
	/// 结点基类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Node<T> : Mutable<T>
		where T : Status, new()
	{
		public Node()
		{
			base.Activate(ActivateType.FilterNode);
		}

		private List<Wire<T>> _inWires = new List<Wire<T>>();

		/// <summary>
		/// 结点的入边
		/// </summary>
		public List<Wire<T>> InWires
		{
			get { return _inWires; }
			set { _inWires = value; }
		}

		private List<Wire<T>> _outWires = new List<Wire<T>>();

		/// <summary>
		/// 结点的出边
		/// </summary>
		public List<Wire<T>> OutWires
		{
			get { return _outWires; }
			set { _outWires = value; }
		}

		public override void Activate(ActivateType type)
		{
			switch (type)
			{
				case ActivateType.FilterNode:
					base.Activate(type);
					break;
				case ActivateType.FilterWire:
					break;
				case ActivateType.FilterUnit:
					base.Activate(type);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 结点状态更新
		/// </summary>
		/// <param name="type"></param>
		public override void Advance(AdvanceType type)
		{
			switch (type)
			{
				case AdvanceType.NodeToWire:
					_FromNodeToWire(_outWires.Select(a => a.Next));
					_ActivateWiresOfNode();
					break;
				case AdvanceType.WireToNode:
					_FromWireToNode(_inWires.Select(a => a.Next));
					Update();
					break;
				default:
					break;
			}			
		}

		protected abstract void _FromWireToNode(IEnumerable<T> inputs);

		protected abstract void _FromNodeToWire(IEnumerable<T> outputs);

		/// <summary>
		/// 状态从结点更新至边，激活边
		/// </summary>
		protected virtual void _ActivateWiresOfNode()
		{
			//激活所有出边
			_outWires.AsParallel().ForAll(a => a.Activate(ActivateType.FilterWire));
		}
	}
}
