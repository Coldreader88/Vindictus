using System;
using UnifiedNetwork.EntityGraph;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	public sealed class RequestUnderingListMessage
	{
		public EntityGraphIdentifier Target
		{
			get
			{
				return this.target;
			}
		}

		public long includedEID
		{
			get
			{
				return this.included;
			}
		}

		public bool isIncluded
		{
			get
			{
				return this.included != -1L;
			}
		}

		public RequestUnderingListMessage()
		{
		}

		public RequestUnderingListMessage(int sid, long eid)
		{
			this.target.serviceID = sid;
			this.target.entityID = eid;
			this.target.category = "";
		}

		public RequestUnderingListMessage(string c, long eid)
		{
			this.target.category = c;
			this.target.entityID = eid;
			this.target.serviceID = 0;
		}

		public RequestUnderingListMessage(int sid, long eid, long ieid)
		{
			this.target.category = "";
			this.target.entityID = eid;
			this.target.serviceID = sid;
			this.included = ieid;
		}

		public RequestUnderingListMessage(EntityGraphIdentifier t)
		{
			this.target = t;
		}

		public RequestUnderingListMessage(EntityGraphIdentifier t, long ieid)
		{
			this.target = t;
			this.included = ieid;
		}

		public override string ToString()
		{
			return string.Format("RequestUnderingListMessage [ {0}/{1} - {2} ]", this.target.category, this.target.serviceID, this.target.entityID);
		}

		private EntityGraphIdentifier target = new EntityGraphIdentifier();

		private long included = -1L;
	}
}
