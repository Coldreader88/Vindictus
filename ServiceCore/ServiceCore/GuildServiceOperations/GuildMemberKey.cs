using System;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public class GuildMemberKey
	{
		public long CID { get; private set; }

		public int CharacterSN { get; private set; }

		public int NexonSN { get; private set; }

		public string CharacterName { get; private set; }

		public GuildMemberKey(long cid, int characterSN, int nexonSN, string characterName)
		{
			this.CID = cid;
			this.CharacterSN = characterSN;
			this.NexonSN = nexonSN;
			this.CharacterName = characterName;
		}

		public override string ToString()
		{
			return string.Format("[CName:{0} CSN:{1} NexonSN:{2} CID:{3}]", new object[]
			{
				this.CharacterName,
				this.CharacterSN,
				this.NexonSN,
				this.CID
			});
		}
	}
}
