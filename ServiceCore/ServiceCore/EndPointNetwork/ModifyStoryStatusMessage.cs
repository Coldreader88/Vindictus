using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ModifyStoryStatusMessage : IMessage
	{
		public string Token { get; set; }

		public int State { get; set; }

		public override string ToString()
		{
			return string.Format("ModifyStoryStatusMessage [ {0} = {1} ]", this.Token, this.State);
		}
	}
}
