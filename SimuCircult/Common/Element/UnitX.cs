using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.UI.Drawing;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Element
{
	public abstract class UnitX<T> : Unit<T>, IDraw
		where T : Status, new()
	{
		public UnitX()
		{
			_L1_border = BorderElement.Create();
			_L1_border[GraphicsDefines.Border_Color] = Color.LightGray;
			_beforeElements.Add(_L1_border);
			OnStateUpdated += UnitX_OnStateUpdated;
			OnValueUpdated += UnitX_OnValueUpdated;
		}

		void UnitX_OnValueUpdated(object sender, MutableValueUpdatedEventArgs<T> e)
		{

		}

		void UnitX_OnStateUpdated(object sender, MutableStateUpdatedEventArgs e)
		{

		}

		private List<IGraphicsElement> _beforeElements = new List<IGraphicsElement>();

		public List<IGraphicsElement> BeforeElements
		{
			get { return _beforeElements; }
			set { _beforeElements = value; }
		}

		private List<IGraphicsElement> _afterElements = new List<IGraphicsElement>();

		public List<IGraphicsElement> AfterElements
		{
			get { return _afterElements; }
			set { _afterElements = value; }
		}

		private Rectangle _absBound = Rectangle.Empty;

		public Rectangle AbsBound
		{
			get { return _absBound; }
			set { _absBound = value; }
		}

		private Rectangle _relBound = Rectangle.Empty;
		public Rectangle RelBound
		{
			get { return _relBound; }
			set { _relBound = value; }
		}

		public Point Location
		{
			get { return _relBound.Location; }
			set { _relBound.Location = value; }
		}

		public Size Size
		{
			get { return _relBound.Size; }
			set { _relBound.Size = value; }
		}

		protected BorderElement _L1_border;

		public virtual void Draw(Rectangle bound)
		{
			foreach (var e in _beforeElements)
			{
				e.GetRenderer().Render(_absBound);
			}
			foreach (var e in Inputs.Union(Hidden).Union(Outputs).Where(a => a is IDraw))
			{
				(e as IDraw).Draw(_absBound);
			}
			foreach (var e in _afterElements)
			{
				e.GetRenderer().Render(_absBound);
			}
		}

		public virtual void Prepare(Rectangle bound)
		{
			_absBound = bound.AdjustBound(_relBound);
			_L1_border[GraphicsDefines.Gdi_Bound] = _absBound;
			foreach (var e in Inputs.Union(Hidden).Union(Outputs).Where(a => a is IDraw))
			{
				(e as IDraw).Prepare(_absBound);
			}
		}
	}
}
