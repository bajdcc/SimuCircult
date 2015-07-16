using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Wire
{
	public class BrokenWire<T> : CommonWire<T>
		where T : Status, new()
	{
		private List<Point> _pointBegin = new List<Point>();

		public List<Point> PointBegin
		{
			get { return _pointBegin; }
			set { _pointBegin = value; }
		}

		private List<Point> _pointEnd = new List<Point>();

		public List<Point> PointEnd
		{
			get { return _pointEnd; }
			set { _pointEnd = value; }
		}

		public BrokenWire()
		{
			_L1_line[GraphicsDefines.LineM_Lazy] = null;
		}

		public override void Prepare(Rectangle bound)
		{
			base.Prepare(bound);
			var begin = (Point)_L1_line[GraphicsDefines.Line_PointBegin];
			_L1_line[GraphicsDefines.Line_PointBeginInternCount] = _pointBegin.Count;
			for (int i = 0; i < _pointBegin.Count; i++)
			{
				begin.Offset(_pointBegin[i]);
				_L1_line[GraphicsDefines.Line_PointBeginAddress + i] = begin;
			}
			var end = (Point)_L1_line[GraphicsDefines.Line_PointEnd];
			_L1_line[GraphicsDefines.Line_PointEndInternCount] = _pointEnd.Count;
			for (int i = _pointEnd.Count - 1; i >= 0; i--)
			{
				end.Offset(_pointEnd[i]);
				_L1_line[GraphicsDefines.Line_PointEndAddress + i] = end;
			}
			_L1_line[GraphicsDefines.LineM_BuildPoint] = null;
		}
	}
}
