using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Drawing
{
	/// <summary>
	/// 图元
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class GraphicsElement<T> : IGraphicsElement
		where T : GraphicsElement<T>, new()
	{
		private IGraphicsElementFactory _factory;
		private IGraphicsRenderer _renderer;

		private Dictionary<int, object> _attr = new Dictionary<int,object>();

		/// <summary>
		/// 图元属性
		/// </summary>
		/// <param name="key">键</param>
		/// <returns>值</returns>
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
				else if (key >= GraphicsDefines.Attr_MessageAddress &&
					key < GraphicsDefines.Attr_MessageAddress + GraphicsDefines.AttrX_MessageLength)
				{
					_renderer.OnElementStateChanged(key, value);
				}
				else
				{

					_attr.Add(key, value);
				}
			}
		}

		/// <summary>
		/// 图元工厂
		/// </summary>
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
			element[GraphicsDefines.Gdi_Enable] = true;
			element.GetRenderer().SetGraphics(Storage.Graphics);
			return element;
		}

		static public void Register()
		{
			Storage.ElementFactory.Add(typeof(T).ToString(), new Factory());
		}

		public void Enable(bool enable)
		{
			this[GraphicsDefines.Gdi_Enable] = enable;
		}

		public bool IsEnabled()
		{
			return (bool)this[GraphicsDefines.Gdi_Enable];
		}
	}
}
