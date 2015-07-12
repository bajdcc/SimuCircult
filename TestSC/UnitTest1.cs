using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimuCircult.Common.Graph;

namespace TestSC
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestSC()
		{
			Circult circult = new Circult();
			circult.CreateSwitchUnit();
		}
	}
}
