using System;
using System.Collections.Generic;
using System.Text;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TiticoreDisplayItemsMessage : IMessage
	{
		private ICollection<ColoredItem> TiticoreRareDisplayItems { get; set; }

		private ICollection<ColoredItem> TiticoreNormalDisplayItems { get; set; }

		private ICollection<ColoredItem> TiticoreKeyItems { get; set; }

		public TiticoreDisplayItemsMessage(ICollection<ColoredItem> titicoreRareDisplayItems, ICollection<ColoredItem> titicoreNormalDisplayItems, ICollection<ColoredItem> titicoreKeyItems)
		{
			this.TiticoreRareDisplayItems = titicoreRareDisplayItems;
			this.TiticoreNormalDisplayItems = titicoreNormalDisplayItems;
			this.TiticoreKeyItems = titicoreKeyItems;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("TiticoreDisplayItemsMessage [ (\n");
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}
	}
}
