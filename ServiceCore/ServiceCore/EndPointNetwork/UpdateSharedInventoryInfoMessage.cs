using System;
using System.Collections.Generic;
using System.Text;
using ServiceCore.ItemServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateSharedInventoryInfoMessage : IMessage
	{
		public UpdateSharedInventoryInfoMessage(ICollection<SlotInfo> infos)
		{
			this.slotInfos = infos;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("UpdateSharedInventoryInfoMessage [ slotInfos = (");
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}

		private ICollection<SlotInfo> slotInfos;
	}
}
