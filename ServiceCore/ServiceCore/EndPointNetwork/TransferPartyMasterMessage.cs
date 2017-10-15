using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TransferPartyMasterMessage : IMessage
	{
		public string NewMasterName { get; set; }

		public override string ToString()
		{
			return string.Format("TransferPartyMaster[ {0} ]", this.NewMasterName);
		}
	}
}
