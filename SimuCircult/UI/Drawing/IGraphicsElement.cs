﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Drawing
{
	public interface IGraphicsElement
	{
		IGraphicsElementFactory GetFactory();
		IGraphicsRenderer GetRenderer();
	}
}
