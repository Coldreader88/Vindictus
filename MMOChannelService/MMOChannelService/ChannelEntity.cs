using System;
using System.Collections.Generic;
using MMOServer;
using UnifiedNetwork.Entity;
using Utility;

namespace MMOChannelService
{
	internal class ChannelEntity
	{
		public Channel Channel { get; private set; }

		public IEntity Entity { get; private set; }

		public int Count
		{
			get
			{
				return this.members.Count;
			}
		}

		public ChannelEntity(Channel channel, IEntity entity)
		{
			this.Channel = channel;
			this.Entity = entity;
			this.Entity.Tag = this;
		}

		public void Enter(ChannelMember member)
		{
			this.members[member.CID] = member;
		}

		public void Leave(ChannelMember member)
		{
			this.members.Remove(member.CID);
		}

		public ChannelMember FindMember(long cid)
		{
			return this.members.TryGetValue(cid);
		}

		private Dictionary<long, ChannelMember> members = new Dictionary<long, ChannelMember>();
	}
}
