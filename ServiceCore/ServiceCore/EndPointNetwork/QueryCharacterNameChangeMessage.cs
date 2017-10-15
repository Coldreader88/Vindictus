using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryCharacterNameChangeMessage : IMessage
	{
		public long CID { get; set; }

		public string RequestName { get; set; }

		public bool IsTrans { get; set; }

		public long ItemID { get; set; }

		public override string ToString()
		{
			return string.Format("QueryCharacterNameChangeMessage [ CID = {0}, RequestName = {1}, IsTrans = {2}, ItemID = {3} ]", new object[]
			{
				this.CID,
				this.RequestName,
				this.IsTrans,
				this.ItemID
			});
		}
	}
}
