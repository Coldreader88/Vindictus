using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class SwapHousingItemMessage : IMessage
	{
		public int From { get; set; }

		public int To { get; set; }

		public override string ToString()
		{
			return string.Format("SwapHousingItemMessage [{0} -> {1}]", this.From, this.To);
		}
	}
}
