using System;
using ServiceCore.CharacterServiceOperations;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingPartyMemberInfo
	{
		public int NexonSN { get; set; }

		public BaseCharacter Character { get; set; }

		public string CharacterName { get; set; }

		public int SlotNumber { get; set; }

		public int Level { get; set; }

		public override string ToString()
		{
			return string.Format("{0}(slot = {1} ch = {2})", this.CharacterName, this.SlotNumber, this.Character);
		}
	}
}
