using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestPetFoodUnshareMessage : IMessage
	{
		public string ItemClass { get; set; }

		public RequestPetFoodUnshareMessage(string itemClass)
		{
			this.ItemClass = itemClass;
		}

		public override string ToString()
		{
			return string.Format("RequestPetFoodUnshareMessage[ itemClass = {0} ]", this.ItemClass);
		}
	}
}
