using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Drawing
{
	/// <summary>
	/// 渲染器
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="U"></typeparam>
	public abstract class GraphicsRenderer<T, U> : IGraphicsRenderer
		where T : GraphicsRenderer<T, U>, new()
		where U : GraphicsElement<U>, new()
	{
		private IGraphicsRendererFactory _factory;
		protected U _element;
		protected Graphics _graphics;

		private Dictionary<int, object> _attr = new Dictionary<int,object>();

		/// <summary>
		/// 渲染器属性（GDI对象）
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
					_attr[key] = value;
				}
				else
				{
					_attr.Add(key, value);
				}
			}
		}

		/// <summary>
		/// 渲染器工厂
		/// </summary>
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
			if ((bool)_element[GraphicsDefines.Gdi_Enable])
				_Render(_AdjustBound(bound));
		}

		protected virtual void _Render(Rectangle bound)
		{

		}

		protected void _ReleaseHandle(int state)
		{
			var obj = this[state] as IDisposable;
			if (obj != null)
			{
				obj.Dispose();
			}
		}

		/// <summary>
		/// 调整大小，将相对坐标转化为绝对坐标
		/// </summary>
		/// <param name="bound"></param>
		/// <returns></returns>
		protected Rectangle _AdjustBound(Rectangle bound)
		{
			return (Rectangle)_element[GraphicsDefines.Gdi_Bound];
		}

		protected virtual void OnChangedGraphics(Graphics oldGraphics, Graphics newGraphics)
		{

		}

		public virtual void OnElementStateChanged(int state, object value)
		{

		}
	}
}
