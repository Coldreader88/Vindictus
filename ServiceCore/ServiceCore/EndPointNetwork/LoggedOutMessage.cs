using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class LoggedOutMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("LoggedOutMessage[]", new object[0]);
		}
	}
}
