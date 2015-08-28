using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using SimuElectricity.Common.Base;
using SimuElectricity.Common.Media;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	/// <summary>
	/// 结点基类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="U"></typeparam>
	public abstract class Node<T, U> : Mutable<T>
		where T : NodeStatus, new()
		where U : WireStatus, new()
	{
		public Node()
		{
			base.Activate(ActivateType.FilterNode);
		}

		private IMedia _media;

		/// <summary>
		/// 结点介质
		/// </summary>
		public IMedia Media
		{
			get { return _media; }
			set { _media = value; }
		}

		private Point _coordinate = Point.Empty;

		/// <summary>
		/// 结点坐标
		/// </summary>
		public Point Coordinate
		{
			get { return _coordinate; }
			set { _coordinate = value; }
		}

		private List<Wire<U, T>> _inWires = new List<Wire<U, T>>();

		/// <summary>
		/// 结点入边
		/// </summary>
		public List<Wire<U, T>> InWires
		{
			get { return _inWires; }
			set { _inWires = value; }
		}

		private List<Wire<U, T>> _outWires = new List<Wire<U, T>>();

		/// <summary>
		/// 结点出边
		/// </summary>
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
					_FromNodeToWire(_outWires);				
					break;
				case AdvanceType.WireToNode:
					_FromWireToNode(_inWires);
					Update();
					break;
				default:
					break;
			}			
		}

		protected abstract void _FromWireToNode(IEnumerable<Wire<U, T>> inputs);

		protected abstract void _FromNodeToWire(IEnumerable<Wire<U, T>> outputs);
	}
}
