using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Drawing
{
	public interface IDraw
	{
		void Draw(Rectangle bound);
		void Prepare(Rectangle bound);
	}
}
