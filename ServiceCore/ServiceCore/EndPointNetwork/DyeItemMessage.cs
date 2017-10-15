using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DyeItemMessage : IMessage
	{
		public bool StartNewSession { get; set; }

		public long ItemID { get; set; }

		public bool IsPremium { get; set; }

		public DyeItemMessage(bool StartNewSession, long itemID, bool IsPremium)
		{
			this.StartNewSession = StartNewSession;
			this.ItemID = itemID;
			this.IsPremium = IsPremium;
		}

		public override string ToString()
		{
			return string.Format("DyeItemMessage[ StartNewSession = {0} ItemID = {1} IsPremium = {2} ]", this.StartNewSession, this.ItemID, this.IsPremium);
		}
	}
}
