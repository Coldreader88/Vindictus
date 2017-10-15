using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateAction : IMessage
	{
		public ActionSync Data { get; set; }
	}
}
