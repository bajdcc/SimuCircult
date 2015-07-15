﻿using SimuCircult.Common.Base;
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
			_L2_background = BackgroundElement.Create();
			_L2_background[GraphicsDefines.Background_Color] = Constants.WindowBackground;
			_L2_background[GraphicsDefines.Background_Shape] = ShapeType.Ellipse;
			_L3_level = TextElement.Create();
			_elements.Add(_L1_border);
			_elements.Add(_L2_background);
			_elements.Add(_L3_level);
			OnStateUpdated += NodeX_OnStateUpdated;
			OnValueUpdated += NodeX_OnValueUpdated;
		}

		protected virtual void NodeX_OnStateUpdated(object sender, MutableStateUpdatedEventArgs e)
		{
			if (!e.Active)
			{
				switch (Local.Code)
				{
					case Constants.LOW_LEVEL:
						_L3_level[GraphicsDefines.Text_Color] = Constants.InactiveLowLevel;
						break;
					case Constants.HIGH_LEVEL:
						_L3_level[GraphicsDefines.Text_Color] = Constants.InactiveHighLevel;
						break;
					default:
						break;
				}
			}
		}

		protected virtual void NodeX_OnValueUpdated(object sender, MutableValueUpdatedEventArgs<T> e)
		{
			switch (e.Status.Code)
			{
				case Constants.LOW_LEVEL:
					_L3_level[GraphicsDefines.Text_Color] = Constants.ActiveLowLevel;
					break;
				case Constants.HIGH_LEVEL:
					_L3_level[GraphicsDefines.Text_Color] = Constants.ActiveHighLevel;
					break;
				default:
					break;
			}
			SetString(e.Status.Code);
		}

		protected virtual void SetString(int code)
		{
			switch (code)
			{
				case Constants.LOW_LEVEL:
					_L3_level[GraphicsDefines.Text_Text] = Constants.LowString;
					break;
				case Constants.HIGH_LEVEL:
					_L3_level[GraphicsDefines.Text_Text] = Constants.HighString;
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

		protected BorderElement _L1_border;
		protected BackgroundElement _L2_background;
		protected TextElement _L3_level;

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
			_L2_background[GraphicsDefines.Gdi_Bound] = _absBound.Deflate(new Size(1, 1));
			_L3_level[GraphicsDefines.Gdi_Bound] = _absBound;
		}
	}
}
