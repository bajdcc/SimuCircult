using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Drawing
{
	public class GraphicsElement<T> : IGraphicsElement
		where T : GraphicsElement<T>, new()
	{
		private IGraphicsElementFactory _factory;
		private IGraphicsRenderer _renderer;

		private Dictionary<int, object> _attr = new Dictionary<int,object>();

		public object this[int key]
		{
			get { return _attr.ContainsKey(key) ? _attr[key] : null; }
			set
			{
				if (_attr.ContainsKey(key))
				{
					if (!_attr[key].Equals(value))
					{
						_attr[key] = value;
						_renderer.OnElementStateChanged(key, value);
					}
				}
				else
				{
					_attr.Add(key, value);
				}
			}
		}

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
			T element = Storage.ElementFactory[typeof(T).ToString()].Create() as T;
			element.GetRenderer().SetGraphics(Storage.Graphics);
			return element;
		}

		static public void Register()
		{
			Storage.ElementFactory.Add(typeof(T).ToString(), new Factory());
		}
	}
}
