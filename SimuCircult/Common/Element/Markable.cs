using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	class Markable
	{
		private Guid _guid;
		private string _name;

		public Markable()
		{
			_guid = new Guid();
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
			set { _guid = value; }
		}
	}
}
