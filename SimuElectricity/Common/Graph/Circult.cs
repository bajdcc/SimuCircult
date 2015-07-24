using SimuElectricity.Common.Base;
using SimuElectricity.Common.Element;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SimuElectricity.Common.Simulator;
using SimuCircult.Common.Graph;

namespace SimuElectricity.Common.Graph
{
	public class Circult : CircultBase<
			NodeStatus,	CommonNode<NodeStatus, WireStatus>,
			WireStatus,	CommonWire<WireStatus, NodeStatus>,
			UnitStatus,	CommonUnit<UnitStatus, NodeStatus, WireStatus>>
	{
		private MarkableArgs _hover;
		private MarkableArgs _focus;
		private Timer _delay;
		private bool _delayTip = false;
		private bool _drag = false;
		private Guid[,] _demonsions;

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
			}
		}

		public void Create()
		{
			_demonsions = new Guid[Defines.WIDTH_COUNT, Defines.HEIGHT_COUNT];
			for (int i = 0; i < Defines.WIDTH_COUNT; i++)
			{
				for (int j = 0; j < Defines.HEIGHT_COUNT; j++)
				{
					var node = CreateNode();
					node.Location = new Point(
						Defines.NODE_OFFSET_X + i * Defines.NODE_WIDTH,
						Defines.NODE_OFFSET_Y + j * Defines.NODE_HEIGHT);
					node.Coordinate = new Point(i, j);
					_demonsions[i, j] = node.Id;
				}
			}
			for (int i = 0; i < Defines.WIDTH_COUNT; i++)
			{
				for (int j = 0; j < Defines.HEIGHT_COUNT; j++)
				{
					if (i + 1 < Defines.WIDTH_COUNT)
					{
						ConnectNode(Nodes[_demonsions[i, j]], Nodes[_demonsions[i + 1, j]]);
						ConnectNode(Nodes[_demonsions[i + 1, j]], Nodes[_demonsions[i, j]], true);
					}
					if (j + 1 < Defines.HEIGHT_COUNT)
					{
						ConnectNode(Nodes[_demonsions[i, j]], Nodes[_demonsions[i, j + 1]]);
						ConnectNode(Nodes[_demonsions[i, j + 1]], Nodes[_demonsions[i, j]], true);
					}
				}
			}
		}

		public void Draw()
		{
			var bound = new Rectangle(Point.Empty, Storage.Size);
			_Prepare(bound);
			_Draw(bound);
		}

		private void _Prepare(Rectangle bound)
		{
			Storage.Graphics.Clear(Constants.WindowBackground);
			foreach (var node in Nodes.Values)
			{
				node.Prepare(bound);
			}
			foreach (var wire in Wires.Values)
			{
				wire.Prepare(bound);
			}
		}

		private void _Draw(Rectangle bound)
		{
			foreach (var wire in Wires.Values.Where(a => !a.External))
			{
				wire.Draw(bound);
			}
			foreach (var node in Nodes.Values)
			{
				node.Draw(bound);
			}
		}

		private MarkableArgs _FindMarkable(Point pt)
		{
			var args = new MarkableArgs() { Pt = pt };
			foreach (var node in Nodes.Values)
			{
				if (node.Handle(HandleType.Test, args) == 0)
				{
					return args;
				}
			}
			foreach (var unit in Units.Values)
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
