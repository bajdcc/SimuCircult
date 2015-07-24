using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Base
{
	public class MutableValueUpdatedEventArgs<T> : EventArgs
	{
		public T Status { get; set; }
	}

	public class MutableStateUpdatedEventArgs : EventArgs
	{
		public bool Active { get; set; }
	}

	public abstract class Mutable<T> : Markable, ISimulate
		where T : Status, new()
	{
		protected event EventHandler<MutableValueUpdatedEventArgs<T>> OnValueUpdated;
		protected event EventHandler<MutableStateUpdatedEventArgs> OnStateUpdated;

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
			if (!_local.Equals(_next))
			{
				_local.CopyFrom(_next);
				var _OnValueUpdated = OnValueUpdated;
				if (OnValueUpdated != null)
				{
					OnValueUpdated(this, new MutableValueUpdatedEventArgs<T>() { Status = _local });
				}
			}
			else
			{
				_active = false;
				var _OnStateUpdated = OnStateUpdated;
				if (_OnStateUpdated != null)
				{
					OnStateUpdated(this, new MutableStateUpdatedEventArgs() { Active = false });
				}
			}
		}

		public virtual void Activate(ActivateType type)
		{
			_active = true;
			var _OnStateUpdated = OnStateUpdated;
			if (_OnStateUpdated != null)
			{
				OnStateUpdated(this, new MutableStateUpdatedEventArgs() { Active = true });
			}
		}

		public virtual void Advance(AdvanceType type)
		{

		}
	}
}
