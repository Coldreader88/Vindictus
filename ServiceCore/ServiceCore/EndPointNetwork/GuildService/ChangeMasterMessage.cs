using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class ChangeMasterMessage : IMessage
	{
		public string NewMasterName { get; set; }

		public ChangeMasterMessage(string newMasterName)
		{
			this.NewMasterName = newMasterName;
		}

		public override string ToString()
		{
			return string.Format("ChangeMasterMessage [to: {0}]", this.NewMasterName);
		}
	}
}
