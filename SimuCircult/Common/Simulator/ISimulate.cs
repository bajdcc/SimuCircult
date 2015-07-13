using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Simulator
{
	public interface ISimulate
	{
		void Update();
		void Activate();
		void Advance();
	}
}
