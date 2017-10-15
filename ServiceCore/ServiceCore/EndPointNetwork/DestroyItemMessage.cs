using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DestroyItemMessage : IMessage
	{
		public string ItemClass
		{
			get
			{
				return this.itemClass;
			}
		}

		public int Num
		{
			get
			{
				return this.num;
			}
		}

		public DestroyItemMessage(string itemClass, int num)
		{
			this.itemClass = itemClass;
			this.num = num;
		}

		public override string ToString()
		{
			return string.Format("DestroyItemMessage[ itemClass = {0} num = {1} ]", this.itemClass, this.num);
		}

		private string itemClass;

		private int num;
	}
}
