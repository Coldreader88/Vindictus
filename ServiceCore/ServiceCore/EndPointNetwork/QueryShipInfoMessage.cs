using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryShipInfoMessage : IMessage
	{
		public long PartyID
		{
			get
			{
				return this.partyID;
			}
		}

		public QueryShipInfoMessage(long partyID)
		{
			this.partyID = partyID;
		}

		public override string ToString()
		{
			return string.Format("QueryShipInfoMessage[ partyID = {0} ]", this.partyID);
		}

		private long partyID;
	}
}
