using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AvatarSynthesisRequestMessage : IMessage
	{
		public long FirstItemID { get; set; }

		public long SecondItemID { get; set; }

		public override string ToString()
		{
			return string.Format("AvatarSynthesisRequestMessage[ FirstItemID = {0}, SecondItemID = {1} ]", this.FirstItemID, this.SecondItemID);
		}
	}
}
