using System;
using UnifiedNetwork.EntityGraph;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	public sealed class ReportUnderingListMessage
	{
		public ReportUnderingListMessage()
		{
			this.serverlist = new EntityGraphIdentifier[0];
		}

		public ReportUnderingListMessage(EntityGraphIdentifier[] t)
		{
			if (t == null)
			{
				this.serverlist = new EntityGraphIdentifier[0];
				return;
			}
			this.serverlist = t;
		}

		public override string ToString()
		{
			return string.Format("ReportUnderingListMessage [ ]", new object[0]);
		}

		public EntityGraphIdentifier[] serverlist;
	}
}
