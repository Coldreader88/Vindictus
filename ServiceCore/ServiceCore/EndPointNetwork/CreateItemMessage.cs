using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CreateItemMessage : IMessage
	{
		public string ItemClass
		{
			get
			{
				return this.itemClass;
			}
		}

		public int ItemNum
		{
			get
			{
				return this.num;
			}
		}

		public CreateItemMessage(string itemClass, int num)
		{
			this.itemClass = itemClass;
			this.num = num;
		}

		public override string ToString()
		{
			return string.Format("CreateItemMessage[ itemClass = {0} num = {1} ]", this.itemClass, this.num);
		}

		private string itemClass;

		private int num;
	}
}
