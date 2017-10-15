using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ItemFailMessage : IMessage
	{
		public HeroesString Message { get; set; }

		public ItemFailMessage(HeroesString msg)
		{
			this.Message = msg;
		}

		public override string ToString()
		{
			return string.Format("ItemFailMessage[ message = {0} ]", this.Message);
		}
	}
}
