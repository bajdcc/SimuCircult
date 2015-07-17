using SimuCircult.Common.Base;
using SimuCircult.Common.Drawing;
using SimuCircult.Common.Element;
using SimuCircult.Common.Node;
using SimuCircult.Common.Simulator;
using SimuCircult.Common.Unit;
using SimuCircult.Common.Wire;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Graph
{
	public class MarkableArgs
	{
		public Point Pt { get; set; }
		public Markable Id { get; set; }
		public IDraw Draw { get; set; }
	}

	public static class CircultHelper
	{
		public static SwitchUnit<Status> CreateSwitchUnit(this Circult circult)
		{
			var unit = circult.CreateUnit<SwitchUnit<Status>>();
			unit.Size = new Size(160, 120);
			var genNode = circult.CreateNode<GenNode<Status>>();
			genNode.Location = new Point(65, 45);
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			outputNode.Location = new Point(130, 45);
			unit.Hidden.Add(genNode);
			unit.Outputs.Add(outputNode);
			unit.Gen = genNode;
			circult.ConnectNode(genNode, outputNode);
			return unit;
		}

		public static OutputUnit<Status> CreateOutputUnit(this Circult circult)
		{
			var unit = circult.CreateUnit<OutputUnit<Status>>();
			unit.Size = new Size(160, 120);
			var inputNode = circult.CreateNode<CommonNode<Status>>();
			inputNode.Location = new Point(0, 45);
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			outputNode.Location = new Point(65, 45);
			unit.Hidden.Add(outputNode);
			unit.Inputs.Add(inputNode);
			circult.ConnectNode(inputNode, outputNode);
			return unit;
		}

		public static OrUnit<Status> CreateOrUnit(this Circult circult, int inputCount = 2)
		{
			var unit = circult.CreateUnit<OrUnit<Status>>();
			unit.Size = new Size(160, 40 * (inputCount + 1));
			var node = circult.CreateNode<OrNode<Status>>();
			node.Location = new Point(65, unit.Size.Height / 2 - 15);
			for (int i = 0; i < inputCount; i++)
			{
				var inputNode = circult.CreateNode<CommonNode<Status>>();
				inputNode.Location = new Point(0, 40 * (i + 1) - 15);
				unit.Inputs.Add(inputNode);
				circult.ConnectNode(inputNode, node);
			}
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			outputNode.Location = new Point(130, 45);
			unit.Hidden.Add(node);
			unit.Outputs.Add(outputNode);
			circult.ConnectNode(node, outputNode);
			return unit;
		}

		public static AndUnit<Status> CreateAndUnit(this Circult circult, int inputCount = 2)
		{
			var unit = circult.CreateUnit<AndUnit<Status>>();
			unit.Size = new Size(160, 40 * (inputCount + 1));
			var node = circult.CreateNode<AndNode<Status>>();
			node.Location = new Point(65, unit.Size.Height / 2 - 15);
			for (int i = 0; i < inputCount; i++)
			{
				var inputNode = circult.CreateNode<CommonNode<Status>>();
				inputNode.Location = new Point(0, 40 * (i + 1) - 15);
				unit.Inputs.Add(inputNode);
				circult.ConnectNode(inputNode, node);
			}
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			outputNode.Location = new Point(130, 45);
			unit.Hidden.Add(node);
			unit.Outputs.Add(outputNode);
			circult.ConnectNode(node, outputNode);
			return unit;
		}

		public static OrNotUnit<Status> CreateOrNotUnit(this Circult circult, int inputCount = 2)
		{
			var unit = circult.CreateUnit<OrNotUnit<Status>>();
			unit.Size = new Size(200, 40 * (inputCount + 1));
			var node = circult.CreateNode<OrNode<Status>>();
			node.Location = new Point(65, unit.Size.Height / 2 - 15);
			for (int i = 0; i < inputCount; i++)
			{
				var inputNode = circult.CreateNode<CommonNode<Status>>();
				inputNode.Location = new Point(0, 40 * (i + 1) - 15);
				unit.Inputs.Add(inputNode);
				circult.ConnectNode(inputNode, node);
			}
			var notNode = circult.CreateNode<NotNode<Status>>();
			notNode.Location = new Point(130, 45);
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			outputNode.Location = new Point(170, 45);
			unit.Hidden.Add(node);
			unit.Hidden.Add(notNode);
			unit.Outputs.Add(outputNode);
			circult.ConnectNode(node, notNode);
			circult.ConnectNode(notNode, outputNode);
			return unit;
		}

		public static AndNotUnit<Status> CreateAndNotUnit(this Circult circult, int inputCount = 2)
		{
			var unit = circult.CreateUnit<AndNotUnit<Status>>();
			unit.Size = new Size(200, 40 * (inputCount + 1));
			var node = circult.CreateNode<AndNode<Status>>();
			node.Location = new Point(65, unit.Size.Height / 2 - 15);
			for (int i = 0; i < inputCount; i++)
			{
				var inputNode = circult.CreateNode<CommonNode<Status>>();
				inputNode.Location = new Point(0, 40 * (i + 1) - 15);
				unit.Inputs.Add(inputNode);
				circult.ConnectNode(inputNode, node);
			}
			var notNode = circult.CreateNode<NotNode<Status>>();
			notNode.Location = new Point(130, 45);
			var outputNode = circult.CreateNode<CommonNode<Status>>();
			outputNode.Location = new Point(170, 45);
			unit.Hidden.Add(node);
			unit.Hidden.Add(notNode);
			unit.Outputs.Add(outputNode);
			circult.ConnectNode(node, notNode);
			circult.ConnectNode(notNode, outputNode);
			return unit;
		}

		public static DisplayUnit<Status> CreateDisplayUnit(this Circult circult)
		{
			var unit = circult.CreateUnit<DisplayUnit<Status>>();
			unit.Size = new Size(600, 120);
			return unit;
		}

		public static SRLockUnit<Status> CreateSRLockUnit(this Circult circult)
		{
			var unit = circult.CreateUnit<SRLockUnit<Status>>();
			unit.Size = new Size(300, 350);
			var in1 = circult.CreateNode<CommonNode<Status>>();
			var in2 = circult.CreateNode<CommonNode<Status>>();
			var an1 = circult.CreateAndNotUnit();
			var an2 = circult.CreateAndNotUnit();
			var o1 = circult.CreateNode<CommonNode<Status>>();
			var o2 = circult.CreateNode<CommonNode<Status>>();
			an1.External = false;
			an2.External = false;
			circult.ConnectNodeMoreInput(in1, an1, 0);
			circult.ConnectNodeMoreInput(in2, an2, 1);
			var bw1 = circult.ConnectUnitMoreInput(an1, an2, 0, new BrokenWire<Status>());
			var bw2 = circult.ConnectUnitMoreInput(an2, an1, 1, new BrokenWire<Status>());
			circult.ConnectNodeMoreOutput(an1, o1, 0);
			circult.ConnectNodeMoreOutput(an2, o2, 0);
			in1.Location = new Point(0, 50);
			in2.Location = new Point(0, 200);
			an1.Location = new Point(50, 50);
			an2.Location = new Point(50, 200);
			o1.Location = new Point(280, 50);
			o2.Location = new Point(280, 200);
			bw1.PointBegin.Add(new Point(0, 60));
			bw1.PointEnd.Add(new Point(0, -40));
			bw2.PointBegin.Add(new Point(0, -60));
			bw2.PointEnd.Add(new Point(0, 40));
			unit.Inputs.Add(in1);
			unit.Inputs.Add(in2);
			unit.Hidden.Add(an1);
			unit.Hidden.Add(an2);
			unit.Outputs.Add(o1);
			unit.Outputs.Add(o2);
			return unit;
		}
	}
}
