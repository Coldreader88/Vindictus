using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class AvatarSynthesisRequest : Operation
	{
		public long FirstItemID { get; set; }

		public long SecondItemID { get; set; }

		public byte Character { get; set; }

		public string SimGrade { get; set; }

		public int SimNum { get; set; }

		public string SimCategory { get; set; }

		public AvatarSynthesisRequest(long firstItemID, long secondItemID, byte character)
		{
			this.FirstItemID = firstItemID;
			this.SecondItemID = secondItemID;
			this.Character = character;
			this.SimNum = 0;
		}

		public AvatarSynthesisRequest(string simGrade, string category, int simNum, byte character)
		{
			this.FirstItemID = 0L;
			this.SecondItemID = 0L;
			this.Character = character;
			this.SimGrade = simGrade;
			this.SimNum = simNum;
			this.SimCategory = category;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
