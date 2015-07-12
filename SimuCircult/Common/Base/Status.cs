using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Base
{
	public class Status
	{
		private int _code = 0;

		public int Code
		{
			get { return _code; }
			set { _code = value; }
		}
	}
}
