using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ReturnFromQuestMessage : IMessage
	{
		public int Order { get; set; }

		public string StorySectorBSP { get; set; }

		public string StorySectorEntity { get; set; }

		public ReturnFromQuestMessage(ReturnFromQuestType order, string bsp, string entity)
		{
			this.Order = (int)order;
			this.StorySectorBSP = bsp;
			this.StorySectorEntity = entity;
		}

		public ReturnFromQuestMessage(ReturnFromQuestType order)
		{
			this.Order = (int)order;
			this.StorySectorBSP = "";
			this.StorySectorEntity = "";
		}

		public override string ToString()
		{
			return string.Format("ReturnFromQuestMessage[ {0} ]", (ReturnFromQuestType)this.Order);
		}
	}
}
