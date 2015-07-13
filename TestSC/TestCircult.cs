using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimuCircult.Common.Graph;
using SimuCircult.Common.Node;
using SimuCircult.Common.Base;

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
			gen.Power = Circult.HIGH_LEVEL;
			var output = circult.CreateOutputUnit();
			circult.ConnectUnitDirect(gen, output);
		}
	}
}
