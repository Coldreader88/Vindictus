using System;
using ServiceCore.CharacterServiceOperations;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PartyMemberInfo
	{
		public int NexonSN { get; set; }

		public BaseCharacter Character { get; set; }

		public string CharacterID { get; set; }

		public int SlotNum { get; set; }

		public int Level { get; set; }

		public ReadyState State { get; set; }

		public int LatestPing { get; set; }

		public int LatestFrameRate { get; set; }

		public bool IsReturn { get; set; }

		public bool IsEventJumping { get; set; }

		public override string ToString()
		{
			return string.Format("{0}(slot = {1} ch = {2} {3})", new object[]
			{
				this.CharacterID,
				this.SlotNum,
				this.Character,
				this.State
			});
		}
	}
}
