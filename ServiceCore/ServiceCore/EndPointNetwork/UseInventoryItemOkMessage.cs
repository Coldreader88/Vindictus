using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UseInventoryItemOkMessage : IMessage
	{
		public string UseItemClass { get; set; }

		public override string ToString()
		{
			return string.Format("UseInventoryItemOkMessage[ ItemClass = {0} ]", this.UseItemClass);
		}
	}
}
