using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryTiticoreDisplayItemsMessage : IMessage
	{
		public string ItemClass { get; private set; }

		public string TargetItemClass { get; private set; }

		public QueryTiticoreDisplayItemsMessage(string itemClass, string targetItemClass)
		{
			this.ItemClass = itemClass;
			this.TargetItemClass = targetItemClass;
		}

		public override string ToString()
		{
			return string.Format("QueryTiticoreDisplayItemsMessage[ ItemClass = {0}, TargetItemClass = {1} ]", this.ItemClass, this.TargetItemClass);
		}
	}
}
