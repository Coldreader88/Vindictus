using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ReturnToTownMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("ReturnToTownMessage[]", new object[0]);
		}
	}
}
