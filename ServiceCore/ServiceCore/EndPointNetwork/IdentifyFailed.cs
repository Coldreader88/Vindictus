using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class IdentifyFailed : IMessage
	{
		public string ErrorMessage { get; set; }

		public IdentifyFailed(string msg)
		{
			this.ErrorMessage = msg;
		}
	}
}
