using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Drawing
{
	public class GraphicsElement<T> : IGraphicsElement
		where T : GraphicsElement<T>, new()
	{
		private IGraphicsElementFactory _factory;
		private IGraphicsRenderer _renderer;

		private class Factory : IGraphicsElementFactory
		{
			public IGraphicsElement Create()
			{
				T element = new T();
				element._factory = this;
				IGraphicsRendererFactory rendererFactory = Storage.RendererFactory[typeof(T).ToString()];
				element._renderer = rendererFactory.Create();
				element._renderer.Start(element);
				return element;
			}
		}

		public IGraphicsElementFactory GetFactory()
		{
			return _factory;
		}

		public IGraphicsRenderer GetRenderer()
		{
			return _renderer;
		}

		static public T Create()
		{
			return Storage.ElementFactory[typeof(T).ToString()].Create() as T;
		}

		static public void Register()
		{
			Storage.ElementFactory.Add(typeof(T).ToString(), new Factory());
		}
	}
}
