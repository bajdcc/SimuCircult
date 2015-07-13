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
			var node = circult.CreateNode<OrNode<Status>>();
			for (int i = 0; i < inputCount; i++)
			{
				var inputNode = circult.CreateNode<CommonNode<Status>>();
				unit.Inputs.Add(inputNode);
				circult.ConnectNode(inputNode, node);
			}
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			unit.Hidden.Add(node);
			unit.Outputs.Add(outputNode);
			circult.ConnectNode(node, outputNode);
			return unit;
		}

		public static AndUnit<Status> CreateAndUnit(this Circult circult, int inputCount = 2)
		{
			var unit = circult.CreateUnit<AndUnit<Status>>();
			var node = circult.CreateNode<AndNode<Status>>();
			for (int i = 0; i < inputCount; i++)
			{
				var inputNode = circult.CreateNode<CommonNode<Status>>();
				unit.Inputs.Add(inputNode);
				circult.ConnectNode(inputNode, node);
			}
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			unit.Hidden.Add(node);
			unit.Outputs.Add(outputNode);
			circult.ConnectNode(node, outputNode);
			return unit;
		}

		public static OrUnit<Status> CreateOrNotUnit(this Circult circult, int inputCount = 2)
		{
			var unit = circult.CreateUnit<OrUnit<Status>>();
			var node = circult.CreateNode<OrNode<Status>>();
			for (int i = 0; i < inputCount; i++)
			{
				var inputNode = circult.CreateNode<CommonNode<Status>>();
				unit.Inputs.Add(inputNode);
				circult.ConnectNode(inputNode, node);
			}
			var notNode = circult.CreateNode<NotNode<Status>>();
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			unit.Hidden.Add(node);
			unit.Hidden.Add(notNode);
			unit.Outputs.Add(outputNode);
			circult.ConnectNode(node, notNode);
			circult.ConnectNode(notNode, outputNode);
			return unit;
		}

		public static AndUnit<Status> CreateAndNotUnit(this Circult circult, int inputCount = 2)
		{
			var unit = circult.CreateUnit<AndUnit<Status>>();
			var node = circult.CreateNode<AndNode<Status>>();
			for (int i = 0; i < inputCount; i++)
			{
				var inputNode = circult.CreateNode<CommonNode<Status>>();
				unit.Inputs.Add(inputNode);
				circult.ConnectNode(inputNode, node);
			}
			var notNode = circult.CreateNode<NotNode<Status>>();
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			unit.Hidden.Add(node);
			unit.Hidden.Add(notNode);
			unit.Outputs.Add(outputNode);
			circult.ConnectNode(node, notNode);
			circult.ConnectNode(notNode, outputNode);
			return unit;
		}
	}
}
