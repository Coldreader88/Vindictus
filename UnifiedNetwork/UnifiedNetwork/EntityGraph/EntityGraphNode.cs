using System;

namespace UnifiedNetwork.EntityGraph
{
	public class EntityGraphNode
	{
		public static readonly int ConnectedNodeListSize = 64;

		public static readonly int BadServiceID = 1048576;

		public static readonly string BadCategory = "#";

		public static EntityGraphNode.NullEntityGraphNode NullNode = new EntityGraphNode.NullEntityGraphNode();

		public static readonly int ServiceEntityID = -1;

		public class NullEntityGraphNode : IEntityGraphNode
		{
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
		}
	}
}
