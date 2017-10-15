using System;

namespace UnifiedNetwork.EntityGraph
{
	public interface IEntityGraphNode
	{
		EntityGraphIdentifier[] ReportConnectedNodeList();

		EntityGraphIdentifier[] ReportConnectedNodeList(long includedEID);

		EntityGraphIdentifier[] ReportConnectedNodeList(EntityGraphIdentifier target);

		IEntityGraphNode GetNode(EntityGraphIdentifier target);

		int ReportUnderingCounts();
	}
}
