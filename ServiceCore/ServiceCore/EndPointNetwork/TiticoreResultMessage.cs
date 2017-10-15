using System;
using System.Collections.Generic;
using System.Text;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TiticoreResultMessage : IMessage
	{
		private ICollection<ColoredItem> ResultItemList { get; set; }

		public TiticoreResultMessage(ICollection<ColoredItem> resultItemList)
		{
			this.ResultItemList = resultItemList;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("TiticoreResultMessage [ (\n");
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}
	}
}
