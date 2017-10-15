using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MarbleResultMessage : IMessage
	{
		public int ResultType { get; set; }

		public string Result { get; set; }

		public MarbleResultMessage(int resultType, string result)
		{
			this.ResultType = resultType;
			this.Result = result;
		}
	}
}
