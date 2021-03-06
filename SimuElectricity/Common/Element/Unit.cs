﻿using SimuCircult.Common.Base;
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
	/// <summary>
	/// 单元基类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="U"></typeparam>
	/// <typeparam name="V"></typeparam>
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

		/// <summary>
		/// 坐标
		/// </summary>
		public Point Coordinate
		{
			get { return _coordinate; }
			set { _coordinate = value; }
		}

		private List<Node<U, V>> _nodes = new List<Node<U, V>>();

		/// <summary>
		/// 结点集合
		/// </summary>
		public List<Node<U, V>> Nodes
		{
			get { return _nodes; }
			set { _nodes = value; }
		}

		private List<Node<U, V>> _interpolateNodes = new List<Node<U, V>>();

		/// <summary>
		/// 插值样本
		/// </summary>
		public List<Node<U, V>> InterpolateNodes
		{
			get { return _interpolateNodes; }
			set { _interpolateNodes = value; }
		}

		private IInterpolating<U> _interpolating;

		/// <summary>
		/// 插值接口
		/// </summary>
		internal IInterpolating<U> Interpolating
		{
			get { return _interpolating; }
			set { _interpolating = value; }
		}

		public virtual void SetInterpolatingInfo()
		{
			if (_interpolating != null)
			{
				_interpolating.SetCoordinate(_coordinate);
				_interpolating.SetPoints(_interpolateNodes.Select(a => a.Local));
			}
		}
	}
}
