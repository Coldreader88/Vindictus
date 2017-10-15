using System;
using System.Collections.Generic;
using Utility;

namespace GuildService
{
	public class GuildChatRoom
	{
		public long GuildID { get; private set; }

		public bool IsEmpty
		{
			get
			{
				return this.members.Count == 0;
			}
		}

		public GuildChatRoom(GuildService service, long guildID)
		{
			this.Service = service;
			this.members = new Dictionary<long, IGuildChatMember>();
			this.GuildID = guildID;
		}

		public void LeaveAllMembers(long guildId)
		{
			List<long> list = new List<long>();
			foreach (KeyValuePair<long, IGuildChatMember> keyValuePair in this.members)
			{
				if (keyValuePair.Value.GuildID == this.GuildID && keyValuePair.Value is GuildChatWebMember)
				{
					list.Add(keyValuePair.Value.CID);
				}
			}
			foreach (long key in list)
			{
				this.members.Remove(key);
			}
			list.Clear();
			list = null;
		}

		public bool JoinGameMember(IGuildChatMember member)
		{
			Log<GuildService>.Logger.WarnFormat("JoinGameMember is called {0}", member.ToString());
			if (this._JoinMember(member, false))
			{
				Log<GuildService>.Logger.WarnFormat("JoinGameMember is called [ Success _JoinMember() : {0} ]", member.ToString());
				if (this.Service.ChatRelay != null)
				{
					Log<GuildService>.Logger.WarnFormat("JoinGameMember is called [ try sendMemberInfo : {0} ]", member.ToString());
					this.Service.ChatRelay.SendMemberInfo(this.GuildID, member.Sender, true);
				}
				return true;
			}
			return false;
		}

		public bool JoinWebMember(GuildChatWebMember member)
		{
			return this._JoinMember(member, true);
		}

		private bool _JoinMember(IGuildChatMember member, bool isWebMember)
		{
			Log<GuildChatRoom>.Logger.InfoFormat("_JoinMember is called. [ {0}, {1}, {2} ]", member.GuildID, member.CID, member.Sender);
			if (this.members.ContainsKey(member.CID))
			{
				Log<GuildChatRoom>.Logger.InfoFormat("_JoinMember is called. [ {0}, {1}, {2} duplicated ]", member.GuildID, member.CID, member.Sender);
				IGuildChatMember guildChatMember = this.members[member.CID];
				GuildChatWebMember guildChatWebMember = guildChatMember as GuildChatWebMember;
				bool flag;
				if (!isWebMember)
				{
					flag = true;
				}
				else
				{
					if (guildChatWebMember == null)
					{
						return false;
					}
					flag = true;
				}
				if (flag)
				{
					Log<GuildChatRoom>.Logger.InfoFormat("_JoinMember is called. [ {0}, {1}, {2} remove ]", member.GuildID, member.CID, member.Sender);
					this.members.Remove(member.CID);
					if (guildChatWebMember != null)
					{
						Log<GuildChatRoom>.Logger.InfoFormat("_JoinMember is called. [ {0}, {1}, {2} kicked ]", member.GuildID, member.CID, member.Sender);
						guildChatWebMember.ChatRelay.KickMember(guildChatWebMember.GuildID, guildChatWebMember.CID);
					}
				}
			}
			this.BroadCastInfoMessage(member.Sender, true);
			this.members.Add(member.CID, member);
			return true;
		}

		public void OnClientChatMessage(long CID, string sender, string message)
		{
			if (this.members.ContainsKey(CID))
			{
				this.BroadCastChatMessage(sender, message);
				if (this.Service.ChatRelay != null)
				{
					this.Service.ChatRelay.SendChat(this.GuildID, sender, message);
				}
			}
		}

		public void OnWebChatMessage(long CID, string sender, string message)
		{
			if (this.members.ContainsKey(CID))
			{
				this.BroadCastChatMessage(sender, message);
			}
		}

		public void OnReceiveChatMessage(long CID, string sender, string message, bool isFromClient)
		{
			if (this.members.ContainsKey(CID))
			{
				this.BroadCastChatMessage(sender, message);
				if (isFromClient && this.Service.ChatRelay != null)
				{
					this.Service.ChatRelay.SendChat(this.GuildID, sender, message);
				}
			}
		}

		public void BroadCastChatMessage(string sender, string message)
		{
			foreach (KeyValuePair<long, IGuildChatMember> keyValuePair in this.members)
			{
				if (keyValuePair.Value is OnlineGuildMember)
				{
					OnlineGuildMember onlineGuildMember = keyValuePair.Value as OnlineGuildMember;
					onlineGuildMember.SendChatMessage(sender, message);
				}
			}
		}

		private void BroadCastInfoMessage(string sender, bool isOnline)
		{
			foreach (KeyValuePair<long, IGuildChatMember> keyValuePair in this.members)
			{
				if (keyValuePair.Value is OnlineGuildMember)
				{
					OnlineGuildMember onlineGuildMember = keyValuePair.Value as OnlineGuildMember;
					onlineGuildMember.SendInfoMessage(sender, isOnline);
				}
			}
		}

		public void LeaveMember(IGuildChatMember member)
		{
			if (this.members.ContainsKey(member.CID))
			{
				if (member == this.members[member.CID])
				{
					this.members.Remove(member.CID);
				}
				else
				{
					member = null;
				}
			}
			if (this.members.Count > 0 && member != null)
			{
				this.BroadCastInfoMessage(member.Sender, false);
				if (member is GuildChatWebMember)
				{
					return;
				}
				if (this.Service.ChatRelay != null)
				{
					this.Service.ChatRelay.SendMemberInfo(this.GuildID, member.Sender, false);
				}
			}
		}

		private GuildService Service;

		private Dictionary<long, IGuildChatMember> members;
	}
}
