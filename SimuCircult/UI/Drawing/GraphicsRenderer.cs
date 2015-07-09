using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimuCircult.UI.Drawing
{
	class GraphicsRenderer<T, U> : IGraphicsRenderer
		where T : GraphicsRenderer<T, U>, new()
		where U : GraphicsElement<U>, new()
	{
		private IGraphicsRendererFactory _factory;
		private U _element;
		private Graphics _graphics;

		private class Factory : IGraphicsRendererFactory
		{
			IGraphicsRenderer Create()
			{
				T renderer = new T();
				renderer._factory = this;
				return renderer;
			}
		}

		public Factory CreateFactory()
		{
			return new Factory();
		}

		static public void Register()
		{
			Storage.RendererFactory.Add(typeof(U).ToString(), new Factory());
			typeof(U).InvokeMember("Register", BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { }, null);
		}

		override public IGraphicsRendererFactory GetFactory()
		{
			return _factory;
		}

		override public void Start(IGraphicsElement element)
		{
			_element = element as U;
			_Start();
		}

		override public void Stop()
		{
			_Stop();
		}

		abstract protected void _Start();

		abstract protected void _Stop();

		override public void SetGraphics(Graphics graphics)
		{
			_graphics = graphics;
		}

		abstract public void Render(Rectangle bound);

		abstract public void OnChanged();
	}
}
