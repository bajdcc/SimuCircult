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
	}
}
