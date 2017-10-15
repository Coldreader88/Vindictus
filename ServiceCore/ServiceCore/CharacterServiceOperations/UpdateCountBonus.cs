using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class UpdateCountBonus : Operation
	{
		public long CID { get; set; }

		public int SamePCRoomCount { get; set; }

		public UpdateCountBonus(long cid, int samePCRoomCount)
		{
			this.CID = cid;
			this.SamePCRoomCount = samePCRoomCount;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
