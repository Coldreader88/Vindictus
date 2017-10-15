using System;

namespace ServiceCore.EndPointNetwork.GuildService
{
	[Serializable]
	public sealed class GuildMemberInfo
	{
		public string CharacterName { get; set; }

		public int GameLevel { get; set; }

		public int Rank { get; set; }

		public long Point { get; set; }

		public int LastLoginTime { get; set; }

		public bool IsOnline { get; set; }

		public GuildMemberInfo(string characterName, int level, GuildMemberRank rank, long point, DateTime lastLoginTime, bool isOnline)
		{
			this.CharacterName = characterName;
			this.GameLevel = level;
			this.Rank = (int)rank;
			this.Point = point;
			DateTime d = (lastLoginTime.Kind == DateTimeKind.Local) ? lastLoginTime.ToUniversalTime() : lastLoginTime;
			this.LastLoginTime = (DateTime.UtcNow - d).Days;
			this.IsOnline = isOnline;
		}

		public static GuildMemberInfo InvalidMember(string characterName)
		{
			return new GuildMemberInfo(characterName, 0, GuildMemberRank.Unknown, 0L, DateTime.Now, false);
		}

		public override string ToString()
		{
			return string.Format("{0}({1}, {2}, {3}, {4}, {5})", new object[]
			{
				this.CharacterName,
				this.GameLevel,
				this.Rank,
				this.Point,
				this.LastLoginTime,
				this.IsOnline
			});
		}
	}
}
