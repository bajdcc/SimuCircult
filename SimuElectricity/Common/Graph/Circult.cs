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
using SimuElectricity.Common.Interpolation;
using SimuElectricity.Common.Helper;
using System.Threading.Tasks;
using SimuCircult.UI.Element;
using System.Diagnostics;

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
		private Guid[,] _demonsionsNode;
		private Guid[,] _demonsionsUnit;
		private bool _taskRunning = false;
		private Stopwatch _stopwatch = new Stopwatch();
		private DisplayUnit<UnitStatus, NodeStatus, WireStatus> _fps;

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
			_Create();
		}

		private void _Create()
		{
			_demonsionsNode = new Guid[Defines.WIDTH_COUNT, Defines.HEIGHT_COUNT];
			_demonsionsUnit = new Guid[Defines.WIDTH_COUNT - 1, Defines.HEIGHT_COUNT - 1];
			for (int i = 0; i < Defines.WIDTH_COUNT; i++)
			{
				for (int j = 0; j < Defines.HEIGHT_COUNT; j++)
				{
					var node = CreateNode();
					node.Location = new Point(
						Defines.NODE_OFFSET_X + i * Defines.NODE_WIDTH,
						Defines.NODE_OFFSET_Y + j * Defines.NODE_HEIGHT);
					node.Coordinate = new Point(i, j);
					_demonsionsNode[i, j] = node.Id;
				}
			}
			for (int i = 0; i < Defines.WIDTH_COUNT; i++)
			{
				for (int j = 0; j < Defines.HEIGHT_COUNT; j++)
				{
					if (i + 1 < Defines.WIDTH_COUNT)
					{
						ConnectNode(Nodes[_demonsionsNode[i, j]], Nodes[_demonsionsNode[i + 1, j]]);
						ConnectNode(Nodes[_demonsionsNode[i + 1, j]], Nodes[_demonsionsNode[i, j]], true);
					}
					if (j + 1 < Defines.HEIGHT_COUNT)
					{
						ConnectNode(Nodes[_demonsionsNode[i, j]], Nodes[_demonsionsNode[i, j + 1]]);
						ConnectNode(Nodes[_demonsionsNode[i, j + 1]], Nodes[_demonsionsNode[i, j]], true);
					}
					if (i + 1 < Defines.WIDTH_COUNT && j + 1 < Defines.HEIGHT_COUNT)
					{
						var unit = CreateUnit();
						unit.Coordinate = new Point(
							Defines.NODE_OFFSET_X + i * Defines.NODE_WIDTH,
							Defines.NODE_OFFSET_Y + j * Defines.NODE_HEIGHT);
						unit.RelBound = new Rectangle(
							unit.Coordinate.X,
							unit.Coordinate.Y,
							Defines.NODE_WIDTH,
							Defines.NODE_HEIGHT);
						for (int x = 0; x < 2; x++)
						{
							for (int y = 0; y < 2; y++)
							{
								unit.Nodes.Add(Nodes[_demonsionsNode[i + x, j + y]]);
							}
						}
						if (i == 0 || j == 0 || i + 2 == Defines.WIDTH_COUNT || j + 2 == Defines.HEIGHT_COUNT)
						{
							unit.Interpolating = new BilinearInterpolation<NodeStatus>();
							unit.InterpolateNodes.AddRange(unit.Nodes);
						}
						else if (i + 2 < Defines.WIDTH_COUNT && j + 2 < Defines.HEIGHT_COUNT)
						{
							unit.Interpolating = new BicubicInterpolation<NodeStatus>();
							for (int x = -1; x < 3; x++)
							{
								for (int y = -1; y < 3; y++)
								{
									unit.InterpolateNodes.Add(Nodes[_demonsionsNode[i + x, j + y]]);
								}
							}
						}
						unit.SetInterpolatingInfo();
						_demonsionsUnit[i, j] = unit.Id;
					}
				}
			}
			_fps = CreateDisplayUnit();
			_fps.Location = new Point(20, 30);
		}

		public void Draw()
		{
			var bound = new Rectangle(Point.Empty, Storage.Size);
			_Interpolate();
			_Prepare(bound);
			_Draw(bound);
		}

		private void _Interpolate()
		{
			var max = Nodes.Values.Max(a => a.Local.Q);
			var min = Nodes.Values.Min(a => a.Local.Q);
			var spacing = (max - min) / Defines.CONTOUR_LINE_COUNT;
			var args = new InterpolationArgs()
			{
				Action = InterpolationArgsAction.Reset,				
				Slice = new Size(Defines.NODE_SUBDIVISION_WIDTH, Defines.NODE_SUBDIVISION_HEIGHT),
				Spacing = new Size(Defines.NODE_WIDTH, Defines.NODE_HEIGHT),
			};
			Units.Values.AsParallel().ForAll(a => a.Interpolate(args));
			args.Action = InterpolationArgsAction.Execute;
			args.Color = LinearGradientColorHelper.GetColor(0);
			args.Estimate = a => a < min;
			Units.Values.AsParallel().ForAll(a => a.Interpolate(args));
			args.Color = LinearGradientColorHelper.GetColor(Defines.CONTOUR_LINE_COUNT + 1);
			args.Estimate = a => a >= max;
			Units.Values.AsParallel().ForAll(a => a.Interpolate(args));
			for (int i = 0; i < Defines.CONTOUR_LINE_COUNT; i++)
			{
				var _min = min + i * spacing;
				var _max = _min + spacing;
				args.Color = LinearGradientColorHelper.GetColor(i + 1);
				args.Estimate = a => a >= _min && a < _max;
				Units.Values.AsParallel().ForAll(a => a.Interpolate(args));
			}
			args.Action = InterpolationArgsAction.Refresh;
			Units.Values.AsParallel().ForAll(a => a.Interpolate(args));
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
			foreach (var unit in Units.Values)
			{
				unit.Prepare(bound);
			}
			_fps.Prepare(bound);
		}

		private void _Draw(Rectangle bound)
		{
			foreach (var unit in Units.Values)
			{
				unit.Draw(bound);
			}
			foreach (var node in Nodes.Values)
			{
				node.Draw(bound);
			}
			foreach (var wire in Wires.Values.Where(a => !a.External))
			{
				wire.Draw(bound);
			}
			_StopWatch();
			_fps.Draw(bound);
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

		private void _StartWatch()
		{
			_stopwatch.Start();
		}

		private void _StopWatch()
		{
			_stopwatch.Stop();
			_fps.Display = string.Format("Frame: {0} ms", _stopwatch.ElapsedMilliseconds);
			_stopwatch.Reset(); 
		}

		public async void OnTimer(Action<Action> invoke)
		{
			if (!_taskRunning)
			{
				_taskRunning = true;
				_taskRunning = await Task.Run(() =>
				{
					_StartWatch();
					Update();
					Draw();					
					invoke(() => Storage.Ctrl.Refresh());
					return false;
				});
			}
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
