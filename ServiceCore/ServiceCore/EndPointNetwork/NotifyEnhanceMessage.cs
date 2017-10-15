using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NotifyEnhanceMessage : IMessage
	{
		public string characterName { get; set; }

		public bool isSuccess { get; set; }

		public int nextEnhanceLevel { get; set; }

		public TooltipItemInfo item { get; set; }

		public override string ToString()
		{
			return string.Format("EnhanceNotifyMessage[ charterName = {0} ]", this.characterName);
		}
	}
}
