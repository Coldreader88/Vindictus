using System;

namespace ServiceCore.EndPointNetwork.MicroPlay
{
	[Serializable]
	public sealed class AllUserJoinCompleteMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("AllUserJoinCompleteMessage[]", new object[0]);
		}
	}
}
