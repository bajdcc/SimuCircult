using SimuCircult.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	abstract class Unit<T> : Mutable<T>
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

		public abstract void Activate();
	}
}
