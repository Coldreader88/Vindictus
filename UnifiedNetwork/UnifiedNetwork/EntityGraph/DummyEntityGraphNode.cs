using System;

namespace UnifiedNetwork.EntityGraph
{
	public class DummyEntityGraphNode : IEntityGraphNode
	{
		public EntityGraphIdentifier Target
		{
			get
			{
				return this.target;
			}
		}

		public DummyEntityGraphNode(string c, long eID)
		{
			EntityGraphIdentifier entityGraphIdentifier = new EntityGraphIdentifier
			{
				serviceID = 1048576,
				entityID = eID,
				category = c
			};
			this.target = entityGraphIdentifier;
		}

		public DummyEntityGraphNode(int sID, long eID)
		{
			EntityGraphIdentifier entityGraphIdentifier = new EntityGraphIdentifier
			{
				serviceID = sID,
				entityID = eID,
				category = "#"
			};
			this.target = entityGraphIdentifier;
		}

		public EntityGraphIdentifier[] ReportConnectedNodeList()
		{
			return null;
		}

		public EntityGraphIdentifier[] ReportConnectedNodeList(EntityGraphIdentifier target)
		{
			return null;
		}

		public EntityGraphIdentifier[] ReportConnectedNodeList(long ieid)
		{
			return null;
		}

		public IEntityGraphNode GetNode(EntityGraphIdentifier target)
		{
			return this;
		}

		public int ReportUnderingCounts()
		{
			return 0;
		}

		private EntityGraphIdentifier target;
	}
}
