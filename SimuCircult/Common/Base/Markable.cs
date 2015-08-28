using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Base
{
	/// <summary>
	/// 命名对象
	/// </summary>
	public class Markable
	{
		private Guid _guid;
		private string _name;

		public Markable()
		{
			_guid = Guid.NewGuid();
			_name = _guid.ToString();
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public Guid Id
		{
			get { return _guid; }
		}

		public override bool Equals(object obj)
		{
			return _guid.Equals((obj as Markable)._guid);
		}

		public override int GetHashCode()
		{
			return _guid.GetHashCode();
		}

		public override string ToString()
		{
			return _name.ToString();
		}
	}
}
