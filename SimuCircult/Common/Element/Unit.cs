using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	/// <summary>
	/// 单元基类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Unit<T> : Node<T>
		where T : Status, new()
	{
		public Unit()
		{
			base.Activate(ActivateType.FilterUnit);
		}

		private List<Node<T>> _inputs = new List<Node<T>>();

		/// <summary>
		/// 单元超图的输入结点抽象
		/// </summary>
		public List<Node<T>> Inputs
		{
			get { return _inputs; }
			set { _inputs = value; }
		}

		private List<Node<T>> _outputs = new List<Node<T>>();

		/// <summary>
		/// 单元超图的输出结点抽象
		/// </summary>
		public List<Node<T>> Outputs
		{
			get { return _outputs; }
			set { _outputs = value; }
		}

		private List<Node<T>> _hidden = new List<Node<T>>();

		/// <summary>
		/// 单元超图的内部结点抽象
		/// </summary>
		public List<Node<T>> Hidden
		{
			get { return _hidden; }
			set { _hidden = value; }
		}

		private bool external = true;

		/// <summary>
		/// 单元是否为顶层单元（可直接遍历）
		/// </summary>
		public bool External
		{
			get { return external; }
			set { external = value; }
		}

		public override void Activate(ActivateType type)
		{
			switch (type)
			{
				case ActivateType.FilterNode:
					break;
				case ActivateType.FilterWire:
					break;
				case ActivateType.FilterUnit:
					if (_Active())
					{
						base.Activate(type);
					}
					break;
				default:
					break;
			}
		}

		public Node<T> GetSingleInput()
		{
			return Inputs.Single();
		}

		public Node<T> GetSingleOutput()
		{
			return Outputs.Single();
		}
		protected virtual bool _Active()
		{
			return false;
		}
	}
}
