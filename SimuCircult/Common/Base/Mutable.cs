using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Base
{
	public abstract class Mutable<T> : Markable, ISimulate
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

		public void Update()
		{
			if (_local != _next)
				_local = _next;
		}

		public abstract void Activate();

		public abstract void Advance();
	}
}
