using System;
using System.Collections.Generic;
using System.Text;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateInventoryInfoMessage : IMessage
	{
		public UpdateInventoryInfoMessage(ICollection<SlotInfo> infos)
		{
			this.slotInfos = infos;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("UpdateInventoryInfoMessage [ slotInfos = (");
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}

		private ICollection<SlotInfo> slotInfos;
	}
}
