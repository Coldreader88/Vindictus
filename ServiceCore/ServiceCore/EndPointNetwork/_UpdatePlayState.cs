using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class _UpdatePlayState : IMessage
	{
		public int State { get; set; }
	}
}
