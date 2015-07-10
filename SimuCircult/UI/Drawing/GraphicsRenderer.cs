using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimuCircult.UI.Drawing
{
	abstract class GraphicsRenderer<T, U> : IGraphicsRenderer
		where T : GraphicsRenderer<T, U>, new()
		where U : GraphicsElement<U>, new()
	{
		private IGraphicsRendererFactory _factory;
		private U _element;
		protected Graphics _graphics;

		private class Factory : IGraphicsRendererFactory
		{
			public IGraphicsRenderer Create()
			{
				T renderer = new T();
				renderer._factory = this;
				return renderer;
			}
		}

		static public void Register()
		{
			Storage.RendererFactory.Add(typeof(U).ToString(), new Factory());
			typeof(U).InvokeMember("Register", BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { }, null);
		}

		public IGraphicsRendererFactory GetFactory()
		{
			return _factory;
		}

		public void Start(IGraphicsElement element)
		{
			_element = element as U;
			_Start();
		}

		public void Stop()
		{
			_Stop();
		}

		abstract protected void _Start();

		abstract protected void _Stop();

		public void SetGraphics(Graphics graphics)
		{
			Graphics tmp = _graphics;
			_graphics = graphics;
			OnChangedGraphics(tmp, graphics);
		}

		abstract public void Render(Rectangle bound);

		abstract protected void OnChangedGraphics(Graphics oldGraphics, Graphics newGraphics);
	}
}
