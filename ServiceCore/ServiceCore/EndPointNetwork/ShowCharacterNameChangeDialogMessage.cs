using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ShowCharacterNameChangeDialogMessage : IMessage
	{
		public long CID { get; set; }

		public long ItemID { get; set; }

		public string Name { get; set; }

		public ShowCharacterNameChangeDialogMessage(long cid, long itemID, string name)
		{
			this.CID = cid;
			this.ItemID = itemID;
			this.Name = name;
		}

		public override string ToString()
		{
			return string.Format("ShowCharacterNameChangeDialogMessage [ CID = {0}, ItemID = {1}, Name = {2} ]", this.CID, this.ItemID, this.Name);
		}
	}
}
