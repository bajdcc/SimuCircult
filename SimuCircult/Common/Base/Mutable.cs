﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Base
{
	class Mutable<T> : Markable
		where T : Status, new()
	{
		private T _local = new T();

		public T Local
		{
			get { return _local; }
			set { _local = value; }
		}

		private T _next = new T();

		public T Next
		{
			get { return _next; }
			set { _next = value; }
		}

		private bool _active = true;

		public bool Active
		{
			get { return _active; }
			set { _active = value; }
		}

		public void Update()
		{
			if (_active)
				_local = _next;
		}
	}
}
