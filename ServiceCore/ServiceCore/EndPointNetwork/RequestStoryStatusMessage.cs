using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestStoryStatusMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("RequestStoryStatusMessage[]", new object[0]);
		}
	}
}
