using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UseInventoryItemFailedMessage : IMessage
	{
		public string UseItemClass { get; set; }

		public string Reason { get; set; }

		public override string ToString()
		{
			return string.Format("UseInventoryItemFailedMessage[ ItemClass = {0} ]", this.UseItemClass);
		}
	}
}
