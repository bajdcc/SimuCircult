using SimuCircult.Common.Base;
using SimuCircult.Common.Element;
using SimuCircult.Common.Node;
using SimuCircult.Common.Unit;
using SimuCircult.Common.Wire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Graph
{
	public static class CircultHelper
	{
		public static SwitchUnit<Status> CreateSwitchUnit(this Circult circult)
		{
			var genUnit = circult.CreateUnit<SwitchUnit<Status>>();
			var genNode = circult.CreateNode<GenNode<Status>>();
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			genUnit.Hidden.Add(genNode);
			circult.ConnectNode(genNode, outputNode);
			return genUnit;
		}
	}
}
