using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AskSecondPasswordMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("AskSecondPasswordMessage [ ]", new object[0]);
		}
	}
}
