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
			var unit = circult.CreateUnit<SwitchUnit<Status>>();
			var genNode = circult.CreateNode<GenNode<Status>>();
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			unit.Hidden.Add(genNode);
			unit.Outputs.Add(outputNode);
			unit.Gen = genNode;
			circult.ConnectNode(genNode, outputNode);
			return unit;
		}

		public static OutputUnit<Status> CreateOutputUnit(this Circult circult)
		{
			var unit = circult.CreateUnit<OutputUnit<Status>>();
			var inputNode = circult.CreateNode<CommonNode<Status>>();
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			unit.Hidden.Add(outputNode);
			unit.Inputs.Add(inputNode);
			circult.ConnectNode(inputNode, outputNode);
			return unit;
		}

		public static OrUnit<Status> CreateOrUnit(this Circult circult, int inputCount = 2)
		{
			var unit = circult.CreateUnit<OrUnit<Status>>();
			var orNode = circult.CreateNode<OrNode<Status>>();
			for (int i = 0; i < inputCount; i++)
			{
				var inputNode = circult.CreateNode<CommonNode<Status>>();
				unit.Inputs.Add(inputNode);
				circult.ConnectNode(inputNode, orNode);
			}
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			unit.Hidden.Add(orNode);
			unit.Outputs.Add(outputNode);
			circult.ConnectNode(orNode, outputNode);
			return unit;
		}
	}
}
