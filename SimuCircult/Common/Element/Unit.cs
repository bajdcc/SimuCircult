using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	public abstract class Unit<T> : Node<T>
		where T : Status, new()
	{
		private List<Node<T>> _inputs = new List<Node<T>>();

		public List<Node<T>> Inputs
		{
			get { return _inputs; }
			set { _inputs = value; }
		}

		private List<Node<T>> _outputs = new List<Node<T>>();

		public List<Node<T>> Outputs
		{
			get { return _outputs; }
			set { _outputs = value; }
		}

		private List<Node<T>> _hidden = new List<Node<T>>();

		public List<Node<T>> Hidden
		{
			get { return _hidden; }
			set { _hidden = value; }
		}

		private bool external = true;

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
