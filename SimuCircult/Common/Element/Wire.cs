using SimuCircult.Common.Base;
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

	public abstract class Wire<T> : Mutable<T>
		where T : Status, new()
	{
		private Node<T> _left;

		public Node<T> Left
		{
			get { return _left; }
			set { _left = value; }
		}

		private Node<T> _right;

		public Node<T> Right
		{
			get { return _right; }
			set { _right = value; }
		}

		private WireType _direction;

		public WireType Direction
		{
			get { return _direction; }
			set { _direction = value; }
		}

		private bool external = false;

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
	}
}
