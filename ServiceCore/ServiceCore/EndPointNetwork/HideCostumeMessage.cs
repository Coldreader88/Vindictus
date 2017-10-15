using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class HideCostumeMessage : IMessage
	{
		public bool HideHead { get; set; }

		public int AvatarPart { get; set; }

		public int AvatarState { get; set; }

		public HideCostumeMessage(bool hideHead)
		{
			this.HideHead = hideHead;
			this.AvatarPart = -1;
			this.AvatarState = -1;
		}

		public override string ToString()
		{
			return string.Format("HideCostumeMessage({0})", this.HideHead);
		}
	}
}
