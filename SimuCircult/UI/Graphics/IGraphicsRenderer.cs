using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.UI.Graphics
{
	interface IGraphicsRenderer
	{
		IGraphicsElementFactory GetFactory();
		void Initialize(IGraphicsElement element);
		void Finalize();
		void SetRenderTarget(IGraphicsRenderTarget renderTarget);
		void Render(Rectangle bound);
		void OnChanged();
	}
}
