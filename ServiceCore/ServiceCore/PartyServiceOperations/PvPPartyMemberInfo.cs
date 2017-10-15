using System;
using ServiceCore.CharacterServiceOperations;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class PvPPartyMemberInfo
	{
		public long CID { get; set; }

		public long FID { get; set; }

		public int NexonSN { get; set; }

		public int Level { get; set; }

		public BaseCharacter BaseCharacter { get; set; }

		public string Name { get; set; }

		public int GuildID { get; set; }

		public string GuildName { get; set; }

		public long MMOLocation { get; set; }

		public long PartyID { get; set; }

		public int RewardBonus { get; set; }

		public PvPPartyMemberInfo(long cid, string name, long fid, int nexonsn, int level, BaseCharacter baseCharacter, int guildID, string guildName, long partyID)
		{
			this.CID = cid;
			this.Name = name;
			this.FID = fid;
			this.NexonSN = nexonsn;
			this.Level = level;
			this.BaseCharacter = baseCharacter;
			this.GuildID = guildID;
			this.GuildName = guildName;
			this.PartyID = partyID;
			this.RewardBonus = 0;
		}
	}
}
