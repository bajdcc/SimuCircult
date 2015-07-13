using SimuCircult.Common.Base;
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

		public Node<T> GetSingleInput()
		{
			return Inputs.Single();
		}

		public Node<T> GetSingleOutput()
		{
			return Outputs.Single();
		}
	}
}
