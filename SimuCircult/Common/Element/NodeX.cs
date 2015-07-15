using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Simulator;
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
	public abstract class NodeX<T> : Node<T>, IDraw
		where T : Status, new()
	{
		public NodeX()
		{
			_relBound.Size = new Size(30, 30);
			_L1_border = BorderElement.Create();
			_L1_border[GraphicsDefines.Border_Shape] = ShapeType.Ellipse;
			_L2_level = TextElement.Create();
			_elements.Add(_L1_border);
			_elements.Add(_L2_level);
			OnStateUpdated += NodeX_OnStateUpdated;
			OnValueUpdated += NodeX_OnValueUpdated;
		}

		void NodeX_OnStateUpdated(object sender, MutableStateUpdatedEventArgs e)
		{
			if (!e.Active)
			{
				_L2_level[GraphicsDefines.Text_Color] = Color.Gray;
			}
		}

		void NodeX_OnValueUpdated(object sender, MutableValueUpdatedEventArgs<T> e)
		{
			switch (e.Status.Code)
			{
				case Constants.LOW_LEVEL:
					_L2_level[GraphicsDefines.Text_Color] = Color.Red;
					_L2_level[GraphicsDefines.Text_Text] = "L";
					break;
				case Constants.HIGH_LEVEL:
					_L2_level[GraphicsDefines.Text_Color] = Color.Blue;
					_L2_level[GraphicsDefines.Text_Text] = "H";
					break;
				default:
					break;
			}
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

		private List<IGraphicsElement> _elements = new List<IGraphicsElement>();

		public List<IGraphicsElement> Elements
		{
			get { return _elements; }
			set { _elements = value; }
		}

		private BorderElement _L1_border;
		private TextElement _L2_level;

		public virtual void Draw(Rectangle bound)
		{			
			foreach (var e in _elements)
			{
				e.GetRenderer().Render(_absBound);
			}
		}

		public virtual void Prepare(Rectangle bound)
		{
			_absBound = bound.AdjustBound(_relBound);
			_L1_border[GraphicsDefines.Gdi_Bound] = _absBound;
			_L2_level[GraphicsDefines.Gdi_Bound] = _absBound.OffsetBound(new Size(5, 5));
		}
	}
}
