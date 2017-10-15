using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class LogOutMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("LogOutMessage[]", new object[0]);
		}
	}
}
