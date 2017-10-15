using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class VocationResetResultMessage : IMessage
	{
		public int Result { get; set; }

		public VocationResetResultMessage(int result)
		{
			this.Result = result;
		}

		public override string ToString()
		{
			return string.Format("VocationResetResultMessage [ {0} ]", this.Result);
		}
	}
}
