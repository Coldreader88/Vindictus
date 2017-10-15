using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SetRaidPartyMessage : IMessage
	{
  //      private int IsRaid;
		//public bool IsRaidParty
		//{
		//	get
		//	{
		//		return this.IsRaid == 1;
		//	}
		//}

		public override string ToString()
		{
			return string.Format("SetRaidPartyMessage", new object[0]);
		}	
	}
}
