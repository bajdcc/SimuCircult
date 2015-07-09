using SimuCircult.Common.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimuCircult.Common.Graph
{
	class Circult
	{
		private Dictionary<Guid, Node> _nodes = new Dictionary<Guid, Node>();
		private Dictionary<Guid, Wire> _wires = new Dictionary<Guid, Wire>();
	}
}
