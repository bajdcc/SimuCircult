using SimuCircult.UI.Global;
using SimuElectricity.Common.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSE
{
	public partial class MainForm : Form
	{
		private Circult circult;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Storage.InitializeGui(new Size(1200, 1200));
			circult = new Circult();
			pictureBox1.Image = Storage.Bitmap;
			Storage.Ctrl = pictureBox1;
			Storage.Tip = toolTip1;
			Create();
		}

		private void Create()
		{
			circult.Create();
			OnTimer();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			OnTimer();
		}

		private void OnTimer()
		{
			circult.OnTimer(this);
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			circult.OnMouseDown(e);
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			circult.OnMouseUp(e);
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			circult.OnMouseMove(e);
		}

		private void pictureBox1_MouseEnter(object sender, EventArgs e)
		{
			circult.OnMouseEnter(pictureBox1.PointToClient(MousePosition));
		}

		private void pictureBox1_MouseLeave(object sender, EventArgs e)
		{
			circult.OnMouseLeave(pictureBox1.PointToClient(MousePosition));
		}
	}
}
