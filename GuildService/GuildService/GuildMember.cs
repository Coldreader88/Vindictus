using System;
using System.Linq;
using GuildService.API;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.GuildServiceOperations;

namespace GuildService
{
	public class GuildMember
	{
		public GuildEntity Parent { get; set; }

		public HeroesGuildMemberInfo HeroesGuildMemberInfo { get; set; }

		public GuildMemberKey Key { get; set; }

		private GuildMemberInfo GuildMemberInfo { get; set; }

		public GuildMember(GuildEntity parent, HeroesGuildMemberInfo groupMemberInfo) : this(parent, groupMemberInfo, new HeroesDataContext())
		{
		}

		public GuildMember(GuildEntity parent, HeroesGuildMemberInfo groupMemberInfo, HeroesDataContext context)
		{
			this.Parent = parent;
			this.SetGroupMemberInfo(groupMemberInfo, context);
		}

		public void SetGroupMemberInfo(HeroesGuildMemberInfo groupMemberInfo, HeroesDataContext context)
		{
			this.HeroesGuildMemberInfo = groupMemberInfo;
			string text = groupMemberInfo.CharacterName.RemoveServerHeader();
			GetCharacterInfoByName getCharacterInfoByName = context.GetCharacterInfoByName(text).FirstOrDefault<GetCharacterInfoByName>();
			int level = (getCharacterInfoByName == null) ? 0 : getCharacterInfoByName.Level;
			long cid = (getCharacterInfoByName == null) ? -1L : getCharacterInfoByName.ID;
			long guildCharacterPoint = context.GetGuildCharacterPoint(cid);
			DateTime? dateTime = null;
			context.GetLatestConnectedDateByName(text, ref dateTime);
			DateTime lastLoginTime = (dateTime == null || dateTime == null) ? DateTime.UtcNow : dateTime.Value;
			this.Key = new GuildMemberKey(cid, (int)groupMemberInfo.CharacterSN, groupMemberInfo.NexonSN, text);
			this.GuildMemberInfo = new GuildMemberInfo(text, level, groupMemberInfo.emGroupUserType.ToGuildMemberRank(), guildCharacterPoint, lastLoginTime, this.Parent.OnlineMembers.ContainsKey(text));
		}

		public void SetLevel(int level)
		{
			this.GuildMemberInfo.GameLevel = level;
		}

		public void AddPoint(int point)
		{
			this.GetGuildMemberInfo().Point += (long)point;
		}

		public long GetPoint()
		{
			return this.GetGuildMemberInfo().Point;
		}

		public GuildMemberInfo GetGuildMemberInfo()
		{
			this.GuildMemberInfo.IsOnline = this.Parent.OnlineMembers.ContainsKey(this.Key.CharacterName);
			return this.GuildMemberInfo;
		}

		public GuildMemberRank Rank
		{
			get
			{
				return (GuildMemberRank)this.GuildMemberInfo.Rank;
			}
		}

		public bool IsValid()
		{
			return this.GuildMemberInfo.GameLevel != 0 && this.Key.CID != -1L;
		}
	}
}
