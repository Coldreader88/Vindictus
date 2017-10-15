using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class LeavePartyMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("LeavePartyMessage[]", new object[0]);
		}
	}
}
