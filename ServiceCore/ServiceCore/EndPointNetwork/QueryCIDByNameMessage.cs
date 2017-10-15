using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryCIDByNameMessage : IMessage
	{
		public string RequestName { get; set; }

		public string ItemClass { get; set; }

		public override string ToString()
		{
			return string.Format("CIDByNameMessage[Name{0}, ItemClass{1}]", this.RequestName, this.ItemClass);
		}
	}
}
