using SimuCircult.Common.Drawing;
using SimuCircult.Common.Graph;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
using SimuElectricity.Common.Base;
using SimuElectricity.Common.Element;
using SimuElectricity.Common.Helper;
using SimuElectricity.Common.Interpolation;
using SimuElectricity.Common.Media;
using SimuElectricity.Common.Simulator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuElectricity.Common.Graph
{
	public class Circult : CircultBase<
			NodeStatus,	CommonNode<NodeStatus, WireStatus>,
			WireStatus,	CommonWire<WireStatus, NodeStatus>,
			UnitStatus,	CommonUnit<UnitStatus, NodeStatus, WireStatus>>
	{
		private MarkableArgs _hover;
		private MarkableArgs _focus;
		private System.Windows.Forms.Timer _delay;
		private bool _delayTip = false;
		private bool _drag = false;
		private CommonNode<NodeStatus, WireStatus>[,] _demonsionsNode;
		private CommonUnit<UnitStatus, NodeStatus, WireStatus>[,] _demonsionsUnit;
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
				_delay.Interval = 1000;
				_delay.Enabled = true;
			}
		}

		public void Create()
		{
			_Create();
		}

		private void _Create()
		{
			_demonsionsNode = new CommonNode<NodeStatus,WireStatus>[Defines.WIDTH_COUNT, Defines.HEIGHT_COUNT];
			_demonsionsUnit = new CommonUnit<UnitStatus,NodeStatus,WireStatus>[Defines.WIDTH_COUNT - 1, Defines.HEIGHT_COUNT - 1];
			for (int i = 0; i < Defines.WIDTH_COUNT; i++)
			{
				for (int j = 0; j < Defines.HEIGHT_COUNT; j++)
				{
					var node = CreateNode();
					node.Location = new Point(
						Defines.NODE_OFFSET_X + i * Defines.NODE_WIDTH,
						Defines.NODE_OFFSET_Y + j * Defines.NODE_HEIGHT);
					node.Coordinate = new Point(i, j);
					if ((1 <= j && j <= 4 && 15 <= i && i <= 45) || (25 <= i && i <= 35 && 5 <= j && j <= 10))
					{
						var media = new CloudMedia();
						media.SetNodeStatus(node.Local);
						node.Media = media;
					}
					else if (24 <= j && j <= 27 && 5 <= i && i <= 55)
					{
						var media = new GroundMedia();
						media.SetNodeStatus(node.Local);
						node.Media = media;
					}
					else if (j == 28 && 5 <= i && i <= 55)
					{
						var media = new GroundMedia();
						media.SetNodeStatus(node.Local);
						node.Media = media;
					}
					else
					{
						var media = new AirMedia();
						media.SetNodeStatus(node.Local);
						node.Media = media;
					}
					_demonsionsNode[i, j] = node;
				}
			}
			for (int i = 0; i < Defines.WIDTH_COUNT; i++)
			{
				for (int j = 0; j < Defines.HEIGHT_COUNT; j++)
				{
					if (i + 1 < Defines.WIDTH_COUNT)
					{
						ConnectNode(_demonsionsNode[i, j], _demonsionsNode[i + 1, j]);
						ConnectNode(_demonsionsNode[i + 1, j], _demonsionsNode[i, j], true);
					}
					if (j + 1 < Defines.HEIGHT_COUNT)
					{
						ConnectNode(_demonsionsNode[i, j], _demonsionsNode[i, j + 1]);
						ConnectNode(_demonsionsNode[i, j + 1], _demonsionsNode[i, j], true);
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
								unit.Nodes.Add(_demonsionsNode[i + x, j + y]);
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
									unit.InterpolateNodes.Add(_demonsionsNode[i + x, j + y]);
								}
							}
						}
						unit.SetInterpolatingInfo();
						_demonsionsUnit[i, j] = unit;
					}
				}
			}
			_fps = CreateDisplayUnit();
			_fps.Location = new Point(20, 30);
		}

		public override void Update()
		{
			Parallel.For(0, Defines.WIDTH_COUNT * Defines.HEIGHT_COUNT, n =>
			{
				int i = n % Defines.WIDTH_COUNT;
				int j = n / Defines.WIDTH_COUNT;
				var node = _demonsionsNode[i, j];
				var coef = node.Media.CalculateElectricField();
				if (coef.HasValue)
				{
					if (Math.Abs(node.Local.Q) > Defines.MIN_Q)
					{
						Parallel.For(0, Defines.WIDTH_COUNT * Defines.HEIGHT_COUNT, m =>
						{
							int x = m % Defines.WIDTH_COUNT;
							int y = m / Defines.WIDTH_COUNT;
							int dx = x - i;
							int dy = y - j;
							if ((dx & dy) != 0)
							{
								var target = _demonsionsNode[x, y];
								var invSqu = Defines.K_ELEC * node.Local.Q * target.Local.Q * InverseSquareCache.Calculate(Math.Abs(dx), Math.Abs(dy));
								target.Local.EX += coef.Value * dx * invSqu;
								target.Local.EY += coef.Value * dy * invSqu;
							}
						});
					}
				}
			});
			base.Update();
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
			//var mid = Math.Max(Math.Abs(max), Math.Abs(min));
			//max = mid;
			//min = -mid;
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
			//foreach (var node in Nodes.Values)
			//{
			//	node.Draw(bound);
			//}
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

		public async void OnTimer(Form form)
		{
			if (!_taskRunning)
			{
				_taskRunning = true;
				_taskRunning = await Task.Run(() =>
				{
					_StartWatch();
					Update();
					Draw();
					form.BeginInvoke(new Action(() => Storage.Ctrl.Refresh()));
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

		private void _OnMouseDown(IDraw draw, MouseEventArgs e)
		{
			switch (e.Button)
			{
				case MouseButtons.Left:
					draw.Handle(HandleType.LeftDown, e.Location);
					break;
				case MouseButtons.Middle:
					draw.Handle(HandleType.MidDown, e.Location);
					break;
				case MouseButtons.Right:
					draw.Handle(HandleType.RightDown, e.Location);
					break;
				default:
					break;
			}
		}

		public void OnMouseDown(MouseEventArgs e)
		{
			var obj = _FindMarkable(e.Location);
			if (_focus != null)
			{
				if (obj != null)
				{
					_OnMouseDown(obj.Draw, e);
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
					_OnMouseDown(obj.Draw, e);
					DoFocus(obj, e.Location);
				}
			}
		}

		private void _OnMouseUp(IDraw draw, MouseEventArgs e)
		{
			switch (e.Button)
			{
				case MouseButtons.Left:
					draw.Handle(HandleType.LeftUp, e.Location);
					break;
				case MouseButtons.Middle:
					draw.Handle(HandleType.MidUp, e.Location);
					break;
				case MouseButtons.Right:
					draw.Handle(HandleType.RightUp, e.Location);
					break;
				default:
					break;
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
				_OnMouseUp(obj.Draw, e);
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
