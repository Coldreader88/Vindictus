using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class InsertBlessStone : Operation
	{
		public BlessStoneType StoneType { get; set; }

		public int OwnerSlot { get; set; }

		public bool IsInsert { get; set; }

		public int RemainFatigue { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
