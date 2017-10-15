using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class JoinHousingMessage : IMessage
	{
		public long TargetID { get; set; }
	}
}
