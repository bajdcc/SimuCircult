using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Graphics
{
	interface IGraphicsElementFactory
	{
		string GetFactoryName();
		IGraphicsElement CreateElement();
	}
}
