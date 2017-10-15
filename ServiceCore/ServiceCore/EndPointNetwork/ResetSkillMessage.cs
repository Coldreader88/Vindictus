using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ResetSkillMessage : IMessage
	{
		public long ItemID { get; set; }

		public string SkillID { get; set; }

		public int AfterRank { get; set; }
	}
}
