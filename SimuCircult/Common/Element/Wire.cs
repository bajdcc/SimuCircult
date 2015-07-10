﻿using SimuCircult.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	enum WireType
	{
		LeftToRight,
		RightToLeft,
		Both,
	}

	abstract class Wire<T> : Markable, Mutable<T>
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
	}
}