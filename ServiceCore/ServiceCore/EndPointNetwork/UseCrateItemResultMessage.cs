using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UseCrateItemResultMessage : IMessage
	{
		public UseCrateItemResultMessage.UseCrateItemResult Result { get; set; }

		private string CrateItem { get; set; }

		private List<string> KeyItems { get; set; }

		public UseCrateItemResultMessage(UseCrateItemResultMessage.UseCrateItemResult result, string crateItem, List<string> keyItems)
		{
			this.Result = result;
			this.CrateItem = crateItem;
			this.KeyItems = keyItems;
		}

		public override string ToString()
		{
			return string.Format("UseCrateItemResultMessage[ Result: {0}, crateItem{1}", this.Result, this.CrateItem);
		}

		public enum UseCrateItemResult
		{
			Unknown,
			Succeeded,
			Failed_NoKey,
			Failed_Unknown
		}
	}
}
