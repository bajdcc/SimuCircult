﻿using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Graph;
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
	public abstract class UnitX<T> : Unit<T>, IInteractive
		where T : Status, new()
	{
		public UnitX()
		{
			_L1_border = BorderElement.Create();
			_prepareElements.Add(_L1_border);
			OnStateUpdated += UnitX_OnStateUpdated;
			OnValueUpdated += UnitX_OnValueUpdated;
		}

		void UnitX_OnValueUpdated(object sender, MutableValueUpdatedEventArgs<T> e)
		{

		}

		void UnitX_OnStateUpdated(object sender, MutableStateUpdatedEventArgs e)
		{

		}

		private List<IGraphicsElement> _prepareElements = new List<IGraphicsElement>();

		public List<IGraphicsElement> PrepareElements
		{
			get { return _prepareElements; }
			set { _prepareElements = value; }
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
			foreach (var e in Inputs.Union(Hidden).Union(Outputs).Where(a => a is IInteractive))
			{
				(e as IInteractive).Draw(_absBound);
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
			foreach (var e in _prepareElements)
			{
				e.GetRenderer().Render(_absBound);
			}
			foreach (var e in Inputs.Union(Hidden).Union(Outputs).Where(a => a is IInteractive))
			{
				(e as IInteractive).Prepare(_absBound);
			}
		}

		public virtual int Handle(HandleType type, object obj)
		{
			var ret = 0;
			if (type == HandleType.Test)
			{
				var args = obj as MarkableArgs;
				if (_absBound.Contains(args.Pt))
				{
					foreach (var k in Inputs.Union(Hidden).Union(Outputs).Where(a => a is IInteractive))
					{
						ret = (k as IInteractive).Handle(type, obj);
						if (ret == 0)
							return 0;
					}
					args.Id = this;
					args.Draw = this;
					return 0;
				}
				return -1;
			}
			var pt = (Point)obj;			
			switch (type)
			{
				case HandleType.LeftUp:
					ret = _Click(pt);
					break;
				case HandleType.Enter:
					ret = _Enter(pt);
					break;
				case HandleType.Leave:
					ret = _Leave(pt);
					break;
				case HandleType.Focus:
					ret = _Focus(pt);
					break;
				case HandleType.LostFocus:
					ret = _LostFocus(pt);
					break;
				case HandleType.Hover:
					ret = _Hover(pt);
					break;
				case HandleType.Drag:
					ret = _Drag(pt);
					break;
				default:
					break;
			}
			if (ret == 0)
				return ret;
			foreach (var k in Inputs.Union(Hidden).Union(Outputs).Where(a => a is IInteractive))
			{
				ret = (k as IInteractive).Handle(type, obj);
				if (ret == 0)
				{
					return 0;
				}
			}
			return -1;
		}

		protected virtual int _Click(Point pt)
		{
			return 0;
		}

		protected virtual int _Enter(Point pt)
		{
			_L1_border[GraphicsDefines.Border_Hover] = true;
			return 0;
		}

		protected virtual int _Leave(Point pt)
		{
			_L1_border[GraphicsDefines.Border_Hover] = false;
			return 0;
		}

		protected virtual int _Focus(Point pt)
		{
			_L1_border[GraphicsDefines.Border_Focus] = true;
			return 0;
		}

		protected virtual int _LostFocus(Point pt)
		{
			_L1_border[GraphicsDefines.Border_Focus] = false;
			return 0;
		}

		protected virtual int _Hover(Point pt)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("Type: {0}\n", GetType());
			sb.AppendFormat("Guid: {0}\n", Id);
			sb.AppendFormat("Name: {0}\n", Name);
			Storage.Tip.ToolTipTitle = "Unit Infomation";
			Storage.Tip.Show(sb.ToString(), Storage.Ctrl, pt);
			return 0;
		}

		protected virtual int _Drag(Point pt)
		{
			_relBound.Offset(pt);
			return 0;
		}
	}
}
