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
		}
	}
}
