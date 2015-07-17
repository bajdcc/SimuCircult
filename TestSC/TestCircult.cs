using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimuCircult.Common.Node;
using SimuCircult.Common.Base;
using SimuCircult.Common.Simulator;
using SimuCircult.Common.Graph;

namespace TestSC
{
	[TestClass]
	public class TestCircult
	{
		[TestMethod]
		public void TestSC1()
		{
			var circult = new Circult();
			var gen = circult.CreateSwitchUnit();
			gen.Power = Constants.HIGH_LEVEL;
			var output = circult.CreateOutputUnit();
			circult.ConnectUnitDirect(gen, output);
			for (int i = 0; i < 5; i++)
			{
				//Console.WriteLine(output.Hidden[0].Local.Code);
				//Console.WriteLine(gen.Outputs[0].Local.Code);
				foreach (var k in circult.Nodes.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.Write("  ");
				foreach (var k in circult.Wires.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.WriteLine();
				circult.Update();
			}
			gen.Power = Constants.LOW_LEVEL;
			for (int i = 0; i < 5; i++)
			{
				//Console.WriteLine(output.Hidden[0].Local.Code);
				//Console.WriteLine(gen.Outputs[0].Local.Code);
				foreach (var k in circult.Nodes.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.Write("  ");
				foreach (var k in circult.Wires.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.WriteLine();
				circult.Update();
			}
		}

		[TestMethod]
		public void TestSC1_1()
		{
			var circult = new Circult();
			var gen = circult.CreateSwitchUnit();
			gen.Power = Constants.HIGH_LEVEL;
			var output = circult.CreateOutputUnit();
			circult.ConnectUnitDirect(gen, output);
			for (int i = 0; i < 5; i++)
			{
				//Console.WriteLine(output.Hidden[0].Local.Code);
				//Console.WriteLine(gen.Outputs[0].Local.Code);
				foreach (var k in circult.Nodes.Values)
				{
					Console.Write(k.Active);
				}
				Console.Write("  ");
				foreach (var k in circult.Wires.Values)
				{
					Console.Write(k.Active);
				}
				Console.Write("  ");
				foreach (var k in circult.Units.Values)
				{
					Console.Write(k.Active);
				}
				Console.WriteLine();
				circult.Update();
			}
			gen.Power = Constants.LOW_LEVEL;
			for (int i = 0; i < 5; i++)
			{
				//Console.WriteLine(output.Hidden[0].Local.Code);
				//Console.WriteLine(gen.Outputs[0].Local.Code);
				foreach (var k in circult.Nodes.Values)
				{
					Console.Write(k.Active);
				}
				Console.Write("  ");
				foreach (var k in circult.Wires.Values)
				{
					Console.Write(k.Active);
				}
				Console.Write("  ");
				foreach (var k in circult.Units.Values)
				{
					Console.Write(k.Active);
				}
				Console.WriteLine();
				circult.Update();
			}
		}

		[TestMethod]
		public void TestSC2()
		{
			var circult = new Circult();
			var gen1 = circult.CreateSwitchUnit();
			gen1.Power = Constants.HIGH_LEVEL;
			var gen2 = circult.CreateSwitchUnit();
			gen2.Power = Constants.HIGH_LEVEL;
			var or = circult.CreateOrUnit();
			var output = circult.CreateOutputUnit();
			circult.ConnectUnitMoreInput(gen1, or, 0);
			circult.ConnectUnitMoreInput(gen2, or, 1);
			circult.ConnectUnitDirect(or, output);
			for (int i = 0; i < 5; i++)
			{
				//Console.WriteLine(output.Hidden[0].Local.Code);
				//Console.WriteLine(gen.Outputs[0].Local.Code);
				foreach (var k in circult.Nodes.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.Write("  ");
				foreach (var k in circult.Wires.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.WriteLine();
				circult.Update();
			}
			gen1.Power = Constants.LOW_LEVEL;
			for (int i = 0; i < 5; i++)
			{
				//Console.WriteLine(output.Hidden[0].Local.Code);
				//Console.WriteLine(gen.Outputs[0].Local.Code);
				foreach (var k in circult.Nodes.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.Write("  ");
				foreach (var k in circult.Wires.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.WriteLine();
				circult.Update();
			}
			gen2.Power = Constants.LOW_LEVEL;
			for (int i = 0; i < 10; i++)
			{
				//Console.WriteLine(output.Hidden[0].Local.Code);
				//Console.WriteLine(gen.Outputs[0].Local.Code);
				foreach (var k in circult.Nodes.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.Write("  ");
				foreach (var k in circult.Wires.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.WriteLine();
				circult.Update();
			}
		}

		[TestMethod]
		public void TestSC3()
		{
			var circult = new Circult();
			var genS = circult.CreateSwitchUnit();
			genS.Power = Constants.HIGH_LEVEL;
			var genR = circult.CreateSwitchUnit();
			genR.Power = Constants.HIGH_LEVEL;
			var an1 = circult.CreateAndNotUnit();
			var an2 = circult.CreateAndNotUnit();
			var o1 = circult.CreateOutputUnit();
			var o2 = circult.CreateOutputUnit();
			circult.ConnectUnitMoreInput(genS, an1, 0);
			circult.ConnectUnitMoreInput(genR, an2, 0);
			circult.ConnectUnitMoreInput(an1, an2, 1);
			circult.ConnectUnitMoreInput(an2, an1, 1);
			circult.ConnectUnitDirect(an1, o1);
			circult.ConnectUnitDirect(an2, o2);
			for (int i = 0; i < 30; i++)
			{
				Console.Write(o1.Hidden[0].Local.Code);
				Console.Write(o2.Hidden[0].Local.Code);
				Console.Write("  ");
				int l = 0;
				foreach (var k in circult.Nodes.Values)
				{
					if (k.Active)
						l++;
					Console.Write(k.Local.Code);
				}
				Console.Write("  ");
				Console.Write(l);
				Console.Write("  ");
				foreach (var k in circult.Wires.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.WriteLine();
				circult.Update();
			}
			Console.WriteLine();
			genS.Power = Constants.LOW_LEVEL;
			for (int i = 0; i < 30; i++)
			{
				Console.Write(o1.Hidden[0].Local.Code);
				Console.Write(o2.Hidden[0].Local.Code);
				Console.Write("  ");
				int l = 0;
				foreach (var k in circult.Nodes.Values)
				{
					if (k.Active)
						l++;
					Console.Write(k.Local.Code);
				}
				Console.Write("  ");
				Console.Write(l);
				Console.Write("  ");
				foreach (var k in circult.Wires.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.WriteLine();
				circult.Update();
			}
			Console.WriteLine();
			genS.Power = Constants.HIGH_LEVEL;
			for (int i = 0; i < 30; i++)
			{
				Console.Write(o1.Hidden[0].Local.Code);
				Console.Write(o2.Hidden[0].Local.Code);
				Console.Write("  ");
				int l = 0;
				foreach (var k in circult.Nodes.Values)
				{
					if (k.Active)
						l++;
					Console.Write(k.Local.Code);
				}
				Console.Write("  ");
				Console.Write(l);
				Console.Write("  ");
				foreach (var k in circult.Wires.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.WriteLine();
				circult.Update();
			}
			Console.WriteLine();
			genR.Power = Constants.LOW_LEVEL;
			for (int i = 0; i < 30; i++)
			{
				Console.Write(o1.Hidden[0].Local.Code);
				Console.Write(o2.Hidden[0].Local.Code);
				Console.Write("  ");
				int l = 0;
				foreach (var k in circult.Nodes.Values)
				{
					if (k.Active)
						l++;
					Console.Write(k.Local.Code);
				}
				Console.Write("  ");
				Console.Write(l);
				Console.Write("  ");
				foreach (var k in circult.Wires.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.WriteLine();
				circult.Update();
			}
			Console.WriteLine();
			genR.Power = Constants.HIGH_LEVEL;
			for (int i = 0; i < 10; i++)
			{
				Console.Write(o1.Hidden[0].Local.Code);
				Console.Write(o2.Hidden[0].Local.Code);
				Console.Write("  ");
				int l = 0;
				foreach (var k in circult.Nodes.Values)
				{
					if (k.Active)
						l++;
					Console.Write(k.Local.Code);
				}
				Console.Write("  ");
				Console.Write(l);
				Console.Write("  ");
				foreach (var k in circult.Wires.Values)
				{
					Console.Write(k.Local.Code);
				}
				Console.WriteLine();
				circult.Update();
			}
		}
	}
}
