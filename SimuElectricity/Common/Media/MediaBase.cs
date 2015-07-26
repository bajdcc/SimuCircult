using SimuCircult.Common.Base;
using SimuElectricity.Common.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuElectricity.Common.Media
{
	public class MediaBase : IMedia
	{
		protected int _id;
		protected NodeStatus _status;

		public MediaBase(int id)
		{
			_id = id;
		}

		public int GetId()
		{
			return _id;
		}

		public virtual bool BreakDownTest(IMedia media, bool breaknode, bool breakdown, double voltage, out double current)
		{
			current = 0;
			return false;
		}

		public virtual void SetNodeStatus(NodeStatus status)
		{
			_status = status;
		}

		public virtual void Advance()
		{
			
		}

		public virtual double? CalculateElectricField()
		{
			return 1.0;
		}
	}
}
