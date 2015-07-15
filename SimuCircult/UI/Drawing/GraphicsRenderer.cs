using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimuCircult.UI.Drawing
{
	public abstract class GraphicsRenderer<T, U> : IGraphicsRenderer
		where T : GraphicsRenderer<T, U>, new()
		where U : GraphicsElement<U>, new()
	{
		private IGraphicsRendererFactory _factory;
		protected U _element;
		protected Graphics _graphics;

		private Dictionary<int, object> _attr = new Dictionary<int,object>();

		public object this[int key]
		{
			get { return _attr.ContainsKey(key) ? _attr[key] : null; }
			set
			{
				if (_attr.ContainsKey(key))
				{
					_attr[key] = value;
				}
				else
				{
					_attr.Add(key, value);
				}
			}
		}

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

		protected virtual void _Start()
		{

		}

		protected virtual void _Stop()
		{

		}

		public void SetGraphics(Graphics graphics)
		{
			Graphics tmp = _graphics;
			_graphics = graphics;
			OnChangedGraphics(tmp, graphics);
		}

		public virtual void Render(Rectangle bound)
		{

		}

		protected virtual void OnChangedGraphics(Graphics oldGraphics, Graphics newGraphics)
		{

		}

		public virtual void OnElementStateChanged(int state, object value)
		{

		}
	}
}
