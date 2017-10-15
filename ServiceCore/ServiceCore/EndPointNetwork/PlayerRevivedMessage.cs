using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PlayerRevivedMessage : IMessage
	{
		public int CasterTag { get; set; }

		public int ReviverTag { get; set; }

		public string Method { get; set; }

		public override string ToString()
		{
			return string.Format("PlayerRevivedMessage[ caster = {0} reviver = {1} method = {2} ]", this.CasterTag, this.ReviverTag, this.Method);
		}
	}
}
