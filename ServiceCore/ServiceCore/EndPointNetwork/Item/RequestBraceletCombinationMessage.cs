using System;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class RequestBraceletCombinationMessage : IMessage
	{
		public long BreaceletItemID { get; set; }

		public long GemstoneItemID { get; set; }

		public int GemstoneIndex { get; set; }

		public bool IsChanging { get; set; }

		public override string ToString()
		{
			return string.Format("RequestBraceletCombinationMessage {0}/{1}/{2}/{3}", new object[]
			{
				this.BreaceletItemID,
				this.GemstoneItemID,
				this.GemstoneIndex,
				this.IsChanging
			});
		}
	}
}
