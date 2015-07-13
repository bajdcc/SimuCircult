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

		private bool _active = false;

		public bool Active
		{
			get { return _active; }
			set { _active = value; }
		}

		public virtual void Update()
		{
			if (_local.Code != _next.Code)
			{
				_local.Code = _next.Code;
			}
			else
			{
				_active = false;
			}
		}

		public virtual void Activate(ActivateType type)
		{
			_active = true;
		}

		public virtual void Advance(AdvanceType type)
		{

		}

		public override string ToString()
		{
			return _active.ToString();
		}
	}
}
