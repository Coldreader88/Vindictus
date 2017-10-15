using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class InsertBlessStoneCompleteMessage : IMessage
	{
		public int Slot { get; set; }

		public List<int> OwnerList { get; set; }

		public List<BlessStoneType> TypeList { get; set; }

		public InsertBlessStoneCompleteMessage()
		{
			this.OwnerList = new List<int>();
			this.TypeList = new List<BlessStoneType>();
		}

		public void AddInfo(BlessStoneType classType, int ownerSlot)
		{
			this.TypeList.Add(classType);
			this.OwnerList.Add(ownerSlot);
		}

		public override string ToString()
		{
			return string.Format("InsertBlessStoneCompleteMessage[ ]", new object[0]);
		}
	}
}
