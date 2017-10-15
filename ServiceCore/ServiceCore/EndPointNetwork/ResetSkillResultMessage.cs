using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ResetSkillResultMessage : IMessage
	{
		public int ResetResult { get; set; }

		public string SkillID { get; set; }

		public int SkillRank { get; set; }

		public int ReturnAP { get; set; }

		public enum ResetResultEnum
		{
			Success,
			NotInTown,
			ResetOtherSkillFirst,
			CannotResetChallenge,
			ItemNotMatch
		}
	}
}
