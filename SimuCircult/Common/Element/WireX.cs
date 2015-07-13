﻿using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	public abstract class WireX<T> : Wire<T>, IDraw
		where T : Status, new()
	{
		private DrawBag _graphics;

		protected DrawBag Graphics
		{
			get { return _graphics; }
			set { _graphics = value; }
		}

		public void SetGraphicsParam(string key, object value)
		{
			_graphics.Dict.Add(key, value);
		}

		public object GetGraphicsParam(string key)
		{
			return _graphics.Dict[key];
		}

		public abstract void Draw();
	}
}