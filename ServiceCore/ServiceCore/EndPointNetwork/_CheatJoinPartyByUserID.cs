using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class _CheatJoinPartyByUserID : IMessage
	{
		public string UserID { get; private set; }

		public override string ToString()
		{
			return string.Format("_CheatJoinShipByUserID [ UserID : {0}]", this.UserID);
		}
	}
}
