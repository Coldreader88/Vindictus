using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MovePartition : IMessage
	{
		public long TargetPartitionID { get; set; }
	}
}
