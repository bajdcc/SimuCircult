using SimuCircult.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Base
{
	public class Status
	{
		//电位
		private int _code = Constants.Initialize();

		public int Code
		{
			get { return _code; }
			set { _code = value; }
		}

		public static T _And<T>(T a, T b)
			where T : Status, new()
		{
			a._code &= b._code;
			return a;
		}

		public static T _Or<T>(T a, T b)
			where T : Status, new()
		{
			a._code |= b._code;
			return a;
		}

		public virtual void CopyFrom(Status obj)
		{
			_code = obj._code;
		}

		public override bool Equals(object obj)
		{
			return _code.Equals((obj as Status)._code);
		}

		public override int GetHashCode()
		{
			return _code.GetHashCode();
		}

		public override string ToString()
		{
			return _code.ToString();
		}
	}
}
