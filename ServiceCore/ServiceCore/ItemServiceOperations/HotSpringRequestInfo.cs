using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class HotSpringRequestInfo : Operation
	{
		public int Channel { get; set; }

		public int TownID { get; set; }

		public HotSpringRequestInfo(int channel, int townID)
		{
			this.Channel = channel;
			this.TownID = townID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
