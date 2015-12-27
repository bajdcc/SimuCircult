using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Graph;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Drawing;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
using SimuElectricity.Common.Base;
using SimuElectricity.Common.Interpolation;
using SimuElectricity.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuElectricity.Common.Element
{
	public abstract class UnitX<T, U, V> : Unit<T, U, V>, IDrawing
		where T : UnitStatus, new()
		where U : NodeStatus, new()
		where V : WireStatus, new()
	{
		public UnitX()
		{
			_relBound.Size = new Size(200, 30);
			_L1_border = BorderElement.Create();
			_L1_border.Enable(false);
			_L2_pixel = PixelElement.Create();
			_elements.Add(_L1_border);
			_elements.Add(_L2_pixel);
		}

		private List<IGraphicsElement> _elements = new List<IGraphicsElement>();

		public List<IGraphicsElement> Elements
		{
			get { return _elements; }
			set { _elements = value; }
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
		protected PixelElement _L2_pixel;

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
			_L2_pixel[GraphicsDefines.Gdi_Bound] = _absBound;
		}

		public virtual int Handle(HandleType type, object obj)
		{
			var ret = 0;
			if (type == HandleType.Test)
			{
				var args = obj as MarkableArgs;
				if (_absBound.Contains(args.Pt))
				{
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
				case HandleType.RightUp:
					ret = _RightClick(pt);
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
			return -1;
		}

		protected virtual int _Click(Point pt)
		{
            StringBuilder sb = new StringBuilder();
            var node = Nodes.First();
            sb.AppendFormat("M: {0}\n", node.Media.GetType().Name);
            sb.AppendFormat("Q: {0}\n", node.Local.Q);
            sb.AppendFormat("B: {0}\n", node.Local.ElecStatus.ToString());
            sb.AppendFormat("EX: {0}\n", node.Local.EX);
            sb.AppendFormat("EY: {0}\n", node.Local.EY);
            var wire = Nodes.First().OutWires.Select(a => a.Local.Current);
            foreach (var w in wire)
            {
                sb.AppendFormat("I: {0}\n", w);
            }
            System.Windows.Forms.MessageBox.Show(sb.ToString(), "Unit Infomation");
            return 0;
		}

		protected virtual int _RightClick(Point pt)
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
            /*StringBuilder sb = new StringBuilder();
			var node = Nodes.First();
			sb.AppendFormat("M: {0}\n", node.Media.GetType().Name);
			sb.AppendFormat("Q: {0}\n", node.Local.Q);
			sb.AppendFormat("B: {0}\n", node.Local.ElecStatus.ToString());
			sb.AppendFormat("EX: {0}\n", node.Local.EX);
			sb.AppendFormat("EY: {0}\n", node.Local.EY);
			var wire = Nodes.First().OutWires.Select(a => a.Local.Current);
			foreach (var w in wire)
			{				
				sb.AppendFormat("I: {0}\n", w);
			}
			Storage.Tip.ToolTipTitle = "Unit Infomation";
			Storage.Tip.Show(sb.ToString(), Storage.Ctrl, pt, 1500);
            System.Windows.Forms.MessageBox.Show("Unit Infomation", sb.ToString());*/
			return 0;
		}

		protected virtual int _Drag(Point pt)
		{
			return 0;
		}

		public override void SetInterpolatingInfo()
		{
			base.SetInterpolatingInfo();
			if (Interpolating != null)
			{
				_L2_pixel[GraphicsDefines.Pixel_Pixels] = Interpolating.GetPixel();
			}
		}

		public void Interpolate(InterpolationArgs args)
		{
			if (Interpolating == null)
			{
				return;
			}
			switch (args.Action)
			{
				case InterpolationArgsAction.Reset:
					Interpolating.Reset();
					break;
				case InterpolationArgsAction.Execute:
					Interpolating.Interpolate(args);
					break;
				case InterpolationArgsAction.Refresh:
					_L2_pixel[GraphicsDefines.Pixel_Refresh] = true;
					break;
				default:
					break;
			}
		}
	}
}
