using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MaxDurabilityRepairItemMessage : IMessage
	{
		public long TargetItemID { get; set; }

		public long SourceItemID { get; set; }

		public MaxDurabilityRepairItemMessage(long targetItemID, long sourceItemID)
		{
			this.TargetItemID = targetItemID;
			this.SourceItemID = sourceItemID;
		}

		public override string ToString()
		{
			return string.Format("MaxArmorRepairItemMessage[ TargetItemID = {0} SourceItemID = {1} ]", this.TargetItemID, this.SourceItemID);
		}
	}
}
