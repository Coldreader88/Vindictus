using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AvatarSynthesisResultMessage : IMessage
	{
		public long ItemID { get; set; }

		public int ResultFlag { get; set; }

		public string Attribute { get; set; }

		public override string ToString()
		{
			return string.Format("AvatarSynthesisResultMessage {0} {1} {2}", this.ItemID, this.ResultFlag, this.Attribute);
		}
	}
}
