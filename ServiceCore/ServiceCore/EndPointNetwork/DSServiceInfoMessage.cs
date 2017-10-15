using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DSServiceInfoMessage : IMessage
	{
		public DSServiceInfoMessage(string msg)
		{
			this.message = msg;
		}

		public override string ToString()
		{
			return string.Format("DSServiceInfo", new object[0]);
		}

		public string message;
	}
}
