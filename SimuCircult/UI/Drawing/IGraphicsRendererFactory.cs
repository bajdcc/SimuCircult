using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Drawing
{
	interface IGraphicsRendererFactory
	{
		IGraphicsRenderer Create();
	}
}
