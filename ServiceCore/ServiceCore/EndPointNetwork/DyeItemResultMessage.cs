using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DyeItemResultMessage : IMessage
	{
		public int Result { get; set; }

		public string ItemClass { get; set; }

		public int Color1 { get; set; }

		public int Color2 { get; set; }

		public int Color3 { get; set; }

		public int TriesLeft { get; set; }

		public DyeItemResultMessage(DyeItemResultMessage.DyeItemResult result)
		{
			this.Result = (int)result;
			this.ItemClass = "";
			this.Color1 = -1;
			this.Color2 = -1;
			this.Color3 = -1;
			this.TriesLeft = 0;
		}

		public DyeItemResultMessage(DyeItemResultMessage.DyeItemResult result, string itemclass, int c1, int c2, int c3, int triesLeft)
		{
			this.Result = (int)result;
			this.ItemClass = itemclass;
			this.Color1 = c1;
			this.Color2 = c2;
			this.Color3 = c3;
			this.TriesLeft = triesLeft;
		}

		public override string ToString()
		{
			return string.Format("DyeItemResultMessage[ result = {0} / {1} / {2}:{3}:{4} ]", new object[]
			{
				this.Result,
				this.ItemClass,
				this.Color1,
				this.Color2,
				this.Color3
			});
		}

		public enum DyeItemResult
		{
			Unknown,
			InvalidItemID,
			NotEquippable,
			NotDyeable,
			InvalidSession,
			PayFeeFailed,
			DyeFailed,
			SessionCreated,
			SessionUsed
		}
	}
}
