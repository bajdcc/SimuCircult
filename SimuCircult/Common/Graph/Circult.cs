using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using SimuCircult.Common.Node;
using SimuCircult.Common.Simulator;
using SimuCircult.Common.Unit;
using SimuCircult.Common.Wire;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimuCircult.Common.Graph
{
	public class Circult : CircultBase<Status, CommonNode<Status>, CommonWire<Status>, CommonUnit<Status>>
	{
		private MarkableArgs _hover;
		private MarkableArgs _focus;
		private Timer _delay;
		private bool _delayTip = false;
		private bool _drag = false;
		private DisplayUnit<Status> _display;

		public Circult()
		{
			_Init();
		}

		private void _Init()
		{
			if (Storage.Graphics != null)
			{
				_delay = Storage.Delay;
				_delay.Tick += Delay_Tick;
				_delay.Interval = 3000;
				_delay.Enabled = true;
				_display = this.CreateDisplayUnit();
				_display.Location = new Point(50, 400);
			}
		}

		public void Draw()
		{
			var bound = new Rectangle(Point.Empty, Storage.Size);
			_Display();
			_Prepare(bound);
			_Draw(bound);
		}

		private void _Display()
		{
			_display.SetText(DisplayOptionType.Active,
				string.Format("Node = {0}, Wire = {1}",
				Nodes.Values.Where(a => a.Active).Count(),
				Wires.Values.Where(a => a.Active).Count()));
			if (_focus != null)
				_display.SetText(DisplayOptionType.Focus, _focus.Id.ToString());
			else
				_display.SetText(DisplayOptionType.Focus, null);
			if (_hover != null)
				_display.SetText(DisplayOptionType.Hover, _hover.Id.ToString());
			else
				_display.SetText(DisplayOptionType.Hover, null);
		}

		private void _Prepare(Rectangle bound)
		{
			Storage.Graphics.Clear(Constants.WindowBackground);
			foreach (var unit in Units.Values.Where(a => a.External))
			{
				unit.Prepare(bound);
			}
			foreach (var wire in Wires.Values)
			{
				wire.Prepare(bound);
			}
		}

		private void _Draw(Rectangle bound)
		{
			foreach (var wire in Wires.Values.Where(a => !a.Active))
			{
				wire.Draw(bound);
			}
			foreach (var wire in Wires.Values.Where(a => a.Active))
			{
				wire.Draw(bound);
			}
			foreach (var unit in Units.Values.Where(a => a.External))
			{
				unit.Draw(bound);
			}
		}

		private MarkableArgs _FindMarkable(Point pt)
		{
			var args = new MarkableArgs() { Pt = pt };
			foreach (var unit in Units.Values.Where(a => a.External))
			{
				if (unit.Handle(HandleType.Test, args) == 0)
				{
					return args;
				}
			}
			return null;
		}

		void Delay_Tick(object sender, EventArgs e)
		{
			if (_hover != null)
			{
				_hover.Draw.Handle(HandleType.Hover, Storage.MousePosition);
			}
			_delayTip = false;
			_delay.Stop();
		}

		public void OnTimer()
		{
			Update();
			Draw();
			Storage.Ctrl.Refresh();
		}

		private void DoFocus(MarkableArgs obj, Point pt)
		{
			_focus = obj;
			_focus.Draw.Handle(HandleType.Enter, pt);
			_drag = true;
		}

		private void DoLostFocus(Point pt)
		{
			_focus.Draw.Handle(HandleType.Leave, pt);
			_focus = null;
			_drag = false;
		}

		public void OnMouseDown(MouseEventArgs e)
		{
			var obj = _FindMarkable(e.Location);
			if (_focus != null)
			{
				if (obj != null)
				{
					obj.Draw.Handle(HandleType.Down, e.Location);
					if (!obj.Id.Equals(_focus.Id))
					{
						DoLostFocus(e.Location);
						DoFocus(obj, e.Location);
					}
				}
				else
				{
					DoLostFocus(e.Location);
				}
			}
			else
			{
				if (obj != null)
				{
					obj.Draw.Handle(HandleType.Down, e.Location);
					DoFocus(obj, e.Location);
				}
			}
		}

		public void OnMouseUp(MouseEventArgs e)
		{
			if (_drag)
			{
				_drag = false;
			}
			var obj = _FindMarkable(e.Location);
			if (obj != null && _focus != null && obj.Id.Equals(_focus.Id))
			{
				obj.Draw.Handle(HandleType.Up, e.Location);
			}
		}

		public void OnMouseMove(MouseEventArgs e)
		{
			var obj = _FindMarkable(e.Location);
			if (_drag)
			{
				if (obj != null && obj.Id.Equals(_focus.Id))
				{
					if (_delayTip)
					{
						_delayTip = false;
						_delay.Stop();
					}
					_focus.Draw.Handle(HandleType.Drag, Point.Subtract(e.Location, (Size)_focus.Pt));
					_focus.Pt = e.Location;
				}
				else
				{
					_drag = false;
					_focus = null;
				}
				return;
			}
			if (_hover != null)
			{
				if (obj != null)
				{
					if (!obj.Id.Equals(_hover.Id))
					{
						DoLeave(e.Location);
						DoEnter(obj, e.Location);
					}
				}
				else
				{
					DoLeave(e.Location);
				}
			}
			else
			{
				if (obj != null)
				{
					DoEnter(obj, e.Location);
				}
			}
		}

		private void DoEnter(MarkableArgs obj, Point pt)
		{
			_hover = obj;
			_hover.Draw.Handle(HandleType.Enter, pt);
			_hover.Draw.Handle(HandleType.Move, pt);
			if (!_delayTip)
			{
				_delayTip = true;
				_delay.Start();
			}
		}

		private void DoLeave(Point pt)
		{
			_drag = false;
			_hover.Draw.Handle(HandleType.Leave, pt);
			_hover = null;
			if (_delayTip)
			{
				_delayTip = false;
				_delay.Stop();
			}
		}

		public void OnMouseEnter(Point pt)
		{
			var obj = _FindMarkable(pt);
			if (obj != null)
			{
				DoEnter(obj, pt);
			}
		}

		public void OnMouseLeave(Point pt)
		{
			if (_hover != null)
			{
				DoLeave(pt);
			}
		}

		public void OnMouseHover(Point pt)
		{
			if (_hover != null)
			{
				_hover.Draw.Handle(HandleType.Hover, pt);
			}
		}
	}
}
