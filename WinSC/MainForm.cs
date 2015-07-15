using SimuCircult.Common.Graph;
using SimuCircult.Common.Simulator;
using SimuCircult.UI.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSC
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
			Storage.InitializeGui(Size);
			circult = new Circult();
			var gen = circult.CreateSwitchUnit();
			gen.Location = new Point(50, 50);
			gen.Power = Constants.HIGH_LEVEL;
			var output = circult.CreateOutputUnit();
			output.Location = new Point(250, 50);
			circult.ConnectUnitDirect(gen, output);
			circult.Initialize();
			pictureBox1.Image = Storage.Bitmap;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			circult.Update();
			circult.Draw();
			pictureBox1.Refresh();
		}
	}
}
