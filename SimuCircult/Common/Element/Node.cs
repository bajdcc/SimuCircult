using SimuCircult.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	public abstract class Node<T> : Mutable<T>
		where T : Status, new()
	{
		private List<Wire<T>> _inWires = new List<Wire<T>>();

		public List<Wire<T>> InWires
		{
			get { return _inWires; }
			set { _inWires = value; }
		}

		private List<Wire<T>> _outWires = new List<Wire<T>>();

		public List<Wire<T>> OutWires
		{
			get { return _outWires; }
			set { _outWires = value; }
		}

		public override void Advance()
		{
			var inputs = _inWires.Select(a => a.Left.Local);
			var outputs = _outWires.Select(a => a.Right.Next);
			_Advance(inputs, outputs);
		}

		protected abstract void _Advance(IEnumerable<T> inputs, IEnumerable<T> outputs);
	}
}
