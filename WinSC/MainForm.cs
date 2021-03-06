﻿using SimuCircult.Common.Graph;
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
			Storage.InitializeGui(new Size(2000, 1800));
			circult = new Circult();
			pictureBox1.Image = Storage.Bitmap;
			Storage.Ctrl = pictureBox1;
			Storage.Tip = toolTip1;
			CreateUnits1();
			CreateUnits2();
		}

		private void CreateUnits1()
		{
			var genS = circult.CreateSwitchUnit();
			genS.Power = Constants.LOW_LEVEL;
			var genR = circult.CreateSwitchUnit();
			genR.Power = Constants.LOW_LEVEL;
			var o1 = circult.CreateOutputUnit();
			var o2 = circult.CreateOutputUnit();
			var SR = circult.CreateSRLockUnit();
			genS.Display = "S";
			genR.Display = "R";
			o1.Display = "Q";
			o2.Display = "Q'";
			SR.Display = "Q[n+1]=!S+RQ[n]";
			genS.Location = new Point(20, 20);
			genR.Location = new Point(20, 170);
			o1.Location = new Point(520, 20);
			o2.Location = new Point(520, 170);
			SR.Location = new Point(200, 20);
			circult.ConnectUnitMoreInput(genS, SR, 0);
			circult.ConnectUnitMoreInput(genR, SR, 1);
			circult.ConnectUnitMoreOutput(SR, o1, 0);
			circult.ConnectUnitMoreOutput(SR, o2, 1);
		}

		private void CreateUnits2()
		{
			var gen = circult.CreateSwitchUnit();
			gen.Power = Constants.LOW_LEVEL;
			var clock = circult.CreateClockUnit();
			var o1 = circult.CreateOutputUnit();
			var o2 = circult.CreateOutputUnit();
			var t = circult.CreateTUnit();
			gen.Display = "T";
			clock.Display = "CP";
			o1.Display = "Q'";
			o2.Display = "Q";
			t.Display = "Q* = T^Q";
			gen.Location = new Point(20, 1080);
			clock.Location = new Point(20, 1290);
			o1.Location = new Point(820, 1080);
			o2.Location = new Point(820, 1290);
			t.Location = new Point(200, 1020);
			circult.ConnectUnitMoreInput(gen, t, 0);
			circult.ConnectUnitMoreInput(clock, t, 1);
			circult.ConnectUnitMoreOutput(t, o1, 0);
			circult.ConnectUnitMoreOutput(t, o2, 1);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			circult.OnTimer();
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
