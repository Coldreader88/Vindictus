using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SelectButtonMessage : IMessage
	{
		public int ButtonIndex { get; set; }

		public SelectButtonMessage(int index)
		{
			this.ButtonIndex = index;
		}

		public override string ToString()
		{
			return string.Format("SelectButtonMessage[ {0} ]", this.ButtonIndex);
		}
	}
}
