using SimuCircult.UI.Drawing;
using SimuCircult.UI.Element;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Renderer
{
	public class LineElementRenderer : PenRenderer<LineElementRenderer, LineElement>
	{
		private List<Point> _pts;
		bool _lazyBuild = false;
		bool _requireBuild = true;

		protected override void _Render(Rectangle bound)
		{
			if (!_lazyBuild)
			{
				_graphics.DrawLine(
					this[GraphicsDefines.Line_Handle] as Pen,
					(Point)_element[GraphicsDefines.Line_PointBegin],
					(Point)_element[GraphicsDefines.Line_PointEnd]);
			}
			else
			{
				_graphics.DrawLines(this[GraphicsDefines.Line_Handle] as Pen, _pts.ToArray());
			}
		}

		private void BuildPointArray()
		{
			if (_pts == null)
				_pts = new List<Point>();
			_pts.Clear();
			var beginCount = (int)_element[GraphicsDefines.Line_PointBeginInternCount];
			var endCount = (int)_element[GraphicsDefines.Line_PointEndInternCount];
			_pts.Add((Point)_element[GraphicsDefines.Line_PointBegin]);
			for (int i = 0; i < beginCount; i++)
			{
				_pts.Add((Point)_element[GraphicsDefines.Line_PointBeginAddress + i]);
			}
			for (int i = endCount - 1; i >= 0; i--)
			{
				_pts.Add((Point)_element[GraphicsDefines.Line_PointEndAddress + i]);
			}
			_pts.Add((Point)_element[GraphicsDefines.Line_PointEnd]);
		}

		public override void OnElementStateChanged(int state, object value)
		{
			switch (state)
			{
				case GraphicsDefines.Line_Color:
				case GraphicsDefines.Line_Width:
				case GraphicsDefines.Line_Style:
				case GraphicsDefines.Line_Join:
					_Destroy();
					_Create();
					break;
				case GraphicsDefines.Line_PointBegin:
				case GraphicsDefines.Line_PointEnd:
				case GraphicsDefines.Line_PointBeginInternCount:
				case GraphicsDefines.Line_PointEndInternCount:
					if (_lazyBuild)
					{
						_requireBuild = true;
					}
					break;
				case GraphicsDefines.LineM_BuildPoint:
					if (_lazyBuild && _requireBuild)
						BuildPointArray();
					break;
				case GraphicsDefines.LineM_Lazy:
					_lazyBuild = true;
					break;
				default:
					break;
			}
		}
	}
}
