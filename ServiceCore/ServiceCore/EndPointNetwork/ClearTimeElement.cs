using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ClearTimeElement
	{
		public int Difficulty { get; set; }

		public int MemberCount { get; set; }

		public int ClearTime { get; set; }

		public int PlayRank { get; set; }

		public int ClearInfo { get; set; }

		public bool IsOld { get; set; }
	}
}
