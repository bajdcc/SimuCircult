using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Graphics
{
	interface IGraphicsRendererFactory
	{
		IGraphicsRenderer Create();
	}
}
