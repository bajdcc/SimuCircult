using SimuCircult.UI.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Element
{
	public class BrushRenderer<T, U> : GraphicsRenderer<T, U>
		where T : GraphicsRenderer<T, U>, new()
		where U : GraphicsElement<U>, new()
	{
		protected virtual void CreateBrush(Graphics graphics)
		{

		}

		protected virtual void DestroyBrush(Graphics graphics)
		{

		}

		protected override void _Start()
		{
			_Create();
		}

		protected override void _Stop()
		{
			_Destroy();
		}

		protected void _Destroy()
		{
			DestroyBrush(_graphics);
		}

		protected void _Create()
		{
			CreateBrush(_graphics);
		}

		protected override void OnChangedGraphics(Graphics oldGraphics, Graphics newGraphics)
		{
			_Destroy();
			_Create();
		}
	}
}
